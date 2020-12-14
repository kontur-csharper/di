using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.Factory
{
    public class TagCloudCreatorFactory : ITagCloudCreatorFactory
    {
        private IWordsForCloudGeneratorFactory wordsForCloudGeneratorFactory;
        private ITagCloudLayouterFactory tagCloudLayouteFactory;
        private IColorGeneratorFactory colorGeneratorFactory;
        private ICloudDrawerFactory cloudDrawerFactory;
        private IPointsFactory pointsFactory;
        private IWordsReader wordsReader;
        private IWordsNormalizer wordsNormalizer;

        public TagCloudCreatorFactory(IWordsForCloudGeneratorFactory wordsForCloudGeneratorFactory,
            IColorGeneratorFactory colorGeneratorFactory,
            ICloudDrawerFactory cloudDrawerFactory,
            ITagCloudLayouterFactory tagCloudLayouteFactory,
            IPointsFactory pointsFactory,
            IWordsReader wordsReader,
            IWordsNormalizer wordsNormalizer)
        {
            this.tagCloudLayouteFactory = tagCloudLayouteFactory;
            this.wordsForCloudGeneratorFactory = wordsForCloudGeneratorFactory;
            this.colorGeneratorFactory = colorGeneratorFactory;
            this.cloudDrawerFactory = cloudDrawerFactory;
            this.pointsFactory = pointsFactory;
            this.wordsNormalizer = wordsNormalizer;
            this.wordsReader = wordsReader;
        }


        public ITagCloudCreator Get(Size pictureSize, Point cloudCenter, Color[] colors, string fontName,
            int maxFontSize,
            string inputFile, string boringWordsFile)
        {
            return new TagCloudCreator(
                wordsForCloudGeneratorFactory.Get(fontName, maxFontSize,
                    tagCloudLayouteFactory.Get(pointsFactory.Get(cloudCenter)),
                    colorGeneratorFactory.Get(colors)), wordsReader, wordsNormalizer,
                cloudDrawerFactory.Get(pictureSize), inputFile, boringWordsFile);
        }
    }
}