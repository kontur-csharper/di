using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyStem.Wrapper.Workers.Grammar.Parsing.Models;
using TagCloud.Core;
using TagCloud.Core.Layouting;
using TagCloud.Core.Output;
using TagCloud.Core.Text;
using TagCloud.Core.Text.Formatting;
using TagCloud.Core.Text.Preprocessing;
using TagCloud.Gui.ImageResizing;
using TagCloud.Gui.InputModels;

namespace TagCloud.Gui
{
    public class App : IApp
    {
        private readonly IUi ui;
        private readonly ITagCloudGenerator cloudGenerator;
        private readonly UserInputOneOptionChoice<IFileWordsReader> readerPicker;
        private readonly UserInputMultipleOptionsChoice<IWordFilter> filterPicker;
        private readonly UserInputOneOptionChoice<IWordConverter> normalizerPicker;
        private readonly UserInputOneOptionChoice<IFileResultWriter> writerPicker;
        private readonly UserInputOneOptionChoice<LayouterType> layouterPicker;
        private readonly UserInputOneOptionChoice<FontSizeSourceType> fontSizeSourcePicker;
        private readonly UserInputOneOptionChoice<FontFamily> fontFamilyPicker;
        private readonly UserInputOneOptionChoice<IImageResizer> imageResizerPicker;
        private readonly UserInputOneOptionChoice<ISpeechPartWordsFilter> speechPartFilterPicker;
        private readonly UserInputField filePathInput;
        private readonly UserInputSizeField centerOffsetPicker;
        private readonly UserInputSizeField betweenWordsDistancePicker;
        private readonly UserInputSizeField imageSizePicker;
        private readonly UserInputOneOptionChoice<ImageFormat> imageFormatPicker;
        private readonly UserInputColor backgroundColorPicker;
        private readonly UserInputColorPalette colorPalettePicker;
        private readonly UserInputMultipleOptionsChoice<MyStemSpeechPart> speechPartPicker;

        public App(IUi ui,
            ITagCloudGenerator cloudGenerator,
            IEnumerable<IFileWordsReader> readers,
            IEnumerable<IWordFilter> filters,
            IEnumerable<IWordConverter> normalizers,
            IEnumerable<IFileResultWriter> writers,
            IEnumerable<IImageResizer> resizers,
            IEnumerable<ISpeechPartWordsFilter> speechFilters)
        {
            this.ui = ui;
            this.cloudGenerator = cloudGenerator;

            readerPicker = UserInput.SingleChoice(ToDictionaryByName(readers), "Words file reader");
            filterPicker = UserInput.MultipleChoice(ToDictionaryByName(filters), "Words filtering method");
            writerPicker = UserInput.SingleChoice(ToDictionaryByName(writers), "Result writing method");
            normalizerPicker = UserInput.SingleChoice(ToDictionaryByName(normalizers), "Words normalization method");
            imageResizerPicker = UserInput.SingleChoice(ToDictionaryByName(resizers), "Resizing method");
            speechPartFilterPicker = UserInput.SingleChoice(ToDictionaryByName(speechFilters), "Speech type filter");
            layouterPicker = UserInput.SingleChoice(DictionaryFromEnum<LayouterType>(), "Layouting algorithm");
            fontSizeSourcePicker = UserInput.SingleChoice(DictionaryFromEnum<FontSizeSourceType>(), "Font size source");

            filePathInput = UserInput.Field("Enter source file path");
            fontFamilyPicker = UserInput.SingleChoice(FontFamily.Families.ToDictionary(x => x.Name), "Font family");
            centerOffsetPicker = UserInput.Size("Cloud center offset", true);
            betweenWordsDistancePicker = UserInput.Size("Minimal distance between rectangles");
            imageSizePicker = UserInput.Size("Result image size");
            backgroundColorPicker = UserInput.Color(Color.Khaki, "Image background color");
            colorPalettePicker = UserInput.ColorPalette("Words color palette", Color.DarkRed);
            speechPartPicker = UserInput.MultipleChoice(DictionaryFromEnum<MyStemSpeechPart>(), "Speech parts rules");

            var formats = new[] {ImageFormat.Gif, ImageFormat.Png, ImageFormat.Bmp, ImageFormat.Jpeg, ImageFormat.Tiff};
            imageFormatPicker = UserInput.SingleChoice(formats.ToDictionary(x => x.ToString()), "Result image format");
        }

        public void Subscribe()
        {
            ui.ExecutionRequested += ExecutionRequested;

            ui.AddUserInput(filePathInput);

            ui.AddUserInput(imageSizePicker);
            AddUserInputOrUseDefault(imageResizerPicker);

            AddUserInputOrUseDefault(layouterPicker);

            ui.AddUserInput(backgroundColorPicker);
            ui.AddUserInput(colorPalettePicker);

            AddUserInputOrUseDefault(fontFamilyPicker);
            AddUserInputOrUseDefault(fontSizeSourcePicker);

            AddUserInputOrUseDefault(speechPartFilterPicker);
            if (speechPartFilterPicker.Available.Any()) ui.AddUserInput(speechPartPicker);

            ui.AddUserInput(filterPicker);
            AddUserInputOrUseDefault(normalizerPicker);

            ui.AddUserInput(centerOffsetPicker);
            ui.AddUserInput(betweenWordsDistancePicker);


            AddUserInputOrUseDefault(readerPicker);
            AddUserInputOrUseDefault(writerPicker);
            AddUserInputOrUseDefault(imageFormatPicker);
        }

        private async void ExecutionRequested()
        {
            using (var lockingContext = ui.StartLockingOperation())
            {
                var words = await ReadWordsAsync(filePathInput.Value, lockingContext.CancellationToken);
                if (lockingContext.CancellationToken.IsCancellationRequested)
                    return;

                var image = await CreateImageAsync(words, lockingContext.CancellationToken);

                ui.OnAfterWordDrawn(image, backgroundColorPicker.Picked);
                if (imageSizePicker.Height > 0 && imageSizePicker.Width > 0)
                {
                    var selectedResizer = imageResizerPicker.Selected.Value;
                    using (var resized = selectedResizer.Resize(image, imageSizePicker.SizeFromCurrent()))
                        FillBackgroundAndSave(resized, backgroundColorPicker.Picked);
                }
                else FillBackgroundAndSave(image, backgroundColorPicker.Picked);
            }
        }

        private async Task<Image> CreateImageAsync(Dictionary<string, int> words, CancellationToken cancellationToken)
        {
            var selectedLayouterType = layouterPicker.Selected.Value;
            var selectedSizeSourceType = fontSizeSourcePicker.Selected.Value;
            var fontFamily = fontFamilyPicker.Selected.Value;

            var resultImage = await cloudGenerator.DrawWordsAsync(
                selectedSizeSourceType,
                selectedLayouterType,
                colorPalettePicker.PickedColors.ToArray(),
                words,
                fontFamily,
                centerOffsetPicker.PointFromCurrent(),
                betweenWordsDistancePicker.SizeFromCurrent(),
                cancellationToken
            );

            return resultImage;
        }

        private async Task<Dictionary<string, int>> ReadWordsAsync(string sourcePath,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                var rawWords = readerPicker.Selected.Value.GetWordsFrom(sourcePath).AsEnumerable();
                var words = filterPicker.Selected
                    .Aggregate(rawWords, (current, filter) => filter.GetValidWordsOnly(current));

                var speechFilter = speechPartFilterPicker.Selected;
                if (!speechFilter.IsEmpty && speechPartPicker.Selected.Any())
                    words = speechFilter.Value.OnlyWithSpeechPart(words, speechPartPicker.Selected.ToHashSet());

                return normalizerPicker.Selected.Value.Normalize(words)
                    .ToLookup(x => x)
                    .ToDictionary(x => x.Key, x => x.Count());
            }, cancellationToken);
        }

        private void FillBackgroundAndSave(Image image, Color backgroundColor)
        {
            using var newImage = new Bitmap(image.Size.Width, image.Size.Height);
            using (var g = Graphics.FromImage(newImage))
            using (var brush = new SolidBrush(backgroundColor))
            {
                g.FillRectangle(brush, new Rectangle(Point.Empty, image.Size));
                g.DrawImage(image, Point.Empty);
            }

            var selectedFormat = imageFormatPicker.Selected.Value;
            writerPicker.Selected.Value.Save(newImage,
                selectedFormat,
                filePathInput.Value + "." + selectedFormat.ToString().ToLower());
        }

        private void AddUserInputOrUseDefault<T>(UserInputOneOptionChoice<T> input)
        {
            if (input.Available.Length > 1)
                ui.AddUserInput(input);
            else
                input.SetSelected(input.Available.Single().Name);
        }

        private static Dictionary<string, TService> ToDictionaryByName<TService>(IEnumerable<TService> source) =>
            source.Where(x => x != null).ToDictionary(x => VisibleName.Get(x.GetType()));

        private static Dictionary<string, TEnum> DictionaryFromEnum<TEnum>() where TEnum : struct, Enum =>
            Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToDictionary(VisibleName.Get);
    }
}