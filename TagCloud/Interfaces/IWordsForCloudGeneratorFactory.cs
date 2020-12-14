using TagsCloudVisualization;

namespace TagCloud.Interfaces
{
    public interface IWordsForCloudGeneratorFactory
    {
        IWordsForCloudGenerator Get(string fontName, int maxFontSize, ITagCloudLayouter tagCloudLayouter,
            IColorGenerator colorGenerator);
    }
}