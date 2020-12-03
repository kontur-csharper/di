using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TagCloud.Core;
using TagCloud.Core.ColoringAlgorithms;
using TagCloud.Core.FileReaders;
using TagCloud.Core.ImageCreators;
using TagCloud.Core.ImageSavers;
using TagCloud.Core.LayoutAlgorithms;
using TagCloud.Core.WordsProcessors;
using TagCloudUI.Infrastructure;
using TagCloudUI.Infrastructure.Selectors;

namespace TagCloudUI.UI
{
    public class ConsoleUI : IUserInterface
    {
        private readonly IReaderSelector readersSelector;
        private readonly IWordsProcessor wordsProcessor;
        private readonly ILayoutAlgorithmSelector layoutAlgorithmSelector;
        private readonly IImageCreator imageCreator;
        private readonly IColoringAlgorithmSelector coloringAlgorithmSelector;
        private readonly IImageSaver imageSaver;

        public ConsoleUI(IReaderSelector readersSelector,
            IWordsProcessor wordsProcessor,
            ILayoutAlgorithmSelector layoutAlgorithmSelector,
            IImageCreator imageCreator,
            IColoringAlgorithmSelector coloringAlgorithmSelector,
            IImageSaver imageSaver)
        {
            this.readersSelector = readersSelector;
            this.wordsProcessor = wordsProcessor;
            this.layoutAlgorithmSelector = layoutAlgorithmSelector;
            this.imageCreator = imageCreator;
            this.coloringAlgorithmSelector = coloringAlgorithmSelector;
            this.imageSaver = imageSaver;
        }

        public void Run(IAppSettings settings)
        {
            using var bitmap = GetCloudImage(settings);
            var savedPath = imageSaver.Save(bitmap, settings.OutputPath, settings.ImageFormat);
            Console.WriteLine($"Tag cloud visualization saved to: {savedPath}");
        }

        private Bitmap GetCloudImage(IAppSettings settings)
        {
            var allWords = GetWordsFromFile(settings.InputPath);
            var processedWords = GetProcessedWords(allWords, settings.WordsCount);
            var layoutInfo = CreateLayout(processedWords, settings.LayoutAlgorithmType, settings.FontName);

            ThrowIfSmallSizeForLayout(settings.ImageWidth, settings.ImageHeight, layoutInfo.Size);

            return CreateImage(layoutInfo, settings.ColoringTheme, settings.FontName);
        }

        private Bitmap CreateImage(LayoutInfo layoutInfo,
            ColoringTheme theme, string fontName)
        {
            if (!coloringAlgorithmSelector.TryGetAlgorithm(theme, out var algorithm))
                throw new ArgumentException(
                    $"There is no such coloring theme: {theme}");

            return imageCreator.Create(algorithm, layoutInfo.Tags, fontName, layoutInfo.Size);
        }


        private static void ThrowIfSmallSizeForLayout(int width, int height, Size layoutSize)
        {
            if (width < layoutSize.Width || height < layoutSize.Height)
                throw new ArgumentException(
                    $"Unable to place TagCloud to this image size: {width}x{height}");
        }

        private IEnumerable<string> GetWordsFromFile(string inputPath)
        {
            var actualExtension = Path.GetExtension(inputPath).TrimStart('.');
            if (!Enum.TryParse(actualExtension, true, out FileExtension extension) ||
                !readersSelector.TryGetReader(extension, out var reader))
                throw new ArgumentException(
                    $"Unable to read file with this extension: {actualExtension}");

            return reader.ReadAllWords(inputPath);
        }

        private List<string> GetProcessedWords(IEnumerable<string> words,
            int wordsCount)
        {
            return wordsProcessor.Process(words, wordsCount).ToList();
        }

        private LayoutInfo CreateLayout(IReadOnlyCollection<string> words,
            LayoutAlgorithmType algorithmType, string fontName)
        {
            if (!layoutAlgorithmSelector.TryGetAlgorithm(algorithmType, out var algorithm))
                throw new ArgumentException($"There is no such algorithm: {algorithmType}");

            var tags = words.Select((word, index) =>
                CreateTag(algorithm, word, index, words.Count, fontName)).ToList();

            return new LayoutInfo(tags, algorithm.GetLayoutSize());
        }

        private static Tag CreateTag(ILayoutAlgorithm algorithm,
            string word, int index, int wordsCount, string fontName)
        {
            var fontSize = wordsCount + 10 - index;
            using var font = new Font(fontName, fontSize);
            var tagSize = TextRenderer.MeasureText(word, font);

            return new Tag(word, algorithm.PutNextRectangle(tagSize), fontSize);
        }
    }
}