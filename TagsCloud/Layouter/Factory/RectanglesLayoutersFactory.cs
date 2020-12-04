using TagsCloud.Factory;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.Layouter.Factory
{
    public class RectanglesLayoutersFactory : ServiceFactory<IRectanglesLayouter>
    {
        private readonly WordConfig wordsConfig;

        public RectanglesLayoutersFactory(WordConfig wordsConfig)
        {
            this.wordsConfig = wordsConfig;
        }

        public override IRectanglesLayouter Create() => services[wordsConfig.LayouterName]();
    }
}
