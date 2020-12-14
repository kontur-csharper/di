using TagCloud.Interfaces;
using TagsCloudVisualization;

namespace TagCloud.Factory
{
    public class WordsForCloudGeneratorFactory : IWordsForCloudGeneratorFactory
    {
        public IWordsForCloudGenerator Get(string fontName, int maxFontSize, ITagCloudLayouter tagCloudLayouter,
            IColorGenerator colorGenerator)
        {
            return new WordsForCloudGenerator(fontName, maxFontSize, tagCloudLayouter, colorGenerator);
        }
    }
}