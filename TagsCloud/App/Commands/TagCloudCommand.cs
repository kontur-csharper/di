using System;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class TagCloudCommand : ICommand
    {
        private readonly Func<Point, ILayoutAlgorithm> algorithmGenerator;
        private readonly FileReader fileReader;
        private readonly IFontFamilyProvider fontFamilyProvider;
        private readonly IImageSizeProvider imageSizeProvider;
        private readonly Func<ILayoutAlgorithm, TagCloudLayouter> layouterGenerator;
        private readonly TagCloudPainter tagCloudPainter;
        private readonly WordFrequency wordFrequency;

        public TagCloudCommand(
            TagCloudPainter tagCloudPainter,
            WordFrequency wordFrequency,
            IImageSizeProvider imageSizeProvider,
            IFontFamilyProvider fontFamilyProvider,
            Func<Point, ILayoutAlgorithm> algorithmGenerator,
            Func<ILayoutAlgorithm, TagCloudLayouter> layouterGenerator,
            FileReader fileReader)
        {
            this.tagCloudPainter = tagCloudPainter;
            this.wordFrequency = wordFrequency;
            this.imageSizeProvider = imageSizeProvider;
            this.fontFamilyProvider = fontFamilyProvider;
            this.algorithmGenerator = algorithmGenerator;
            this.layouterGenerator = layouterGenerator;
            this.fileReader = fileReader;
        }

        public string Name { get; } = "tagcloud";
        public string Description { get; } = "tagcloud <fileName>      # creating tag cloud from file";

        public Result<None> Execute(string[] args)
        {
            var filePath = string.Join(" ", args);
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            var layouter = GetTagCloudLayouter();
            return fileReader.ReadLines(filePath)
                .Then(lines => wordFrequency.Get(lines))
                .Then(frequencies =>
                    frequencies
                        .OrderByDescending(x => x.Value)
                        .Select(y => PlaceWord(y.Key, y.Value, layouter, graphics)))
                .Then(placedWords => tagCloudPainter.Paint(placedWords));
        }

        private Word PlaceWord(string word, double frequency, TagCloudLayouter layouter, Graphics graphics)
        {
            var font = new Font(fontFamilyProvider.FontFamily, GetFontSize(frequency));
            var wordSize = graphics.MeasureString(word, font).ToSize();
            var wordRectangle = layouter.PutNextRectangle(wordSize);
            return new Word(word, font, wordRectangle);
        }

        private int GetFontSize(double frequency) => (int) (frequency * 100 + 10);

        private TagCloudLayouter GetTagCloudLayouter()
        {
            var center = imageSizeProvider.ImageSize.GetCenter();
            var algorithm = algorithmGenerator(center);
            return layouterGenerator(algorithm);
        }
    }
}