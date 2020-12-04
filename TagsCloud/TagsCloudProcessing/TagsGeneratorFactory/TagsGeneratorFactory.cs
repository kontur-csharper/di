using TagsCloud.Factory;
using TagsCloud.TagsCloudProcessing.TagsGenerators;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TagsCloudProcessing.TagsGeneratorFactory
{
    public class TagsGeneratorFactory : ServiceFactory<ITagsGenerator>
    {
        private readonly WordConfig wordsConfig;

        public TagsGeneratorFactory(WordConfig wordsConfig)
        {
            this.wordsConfig = wordsConfig;
        }

        public override ITagsGenerator Create() => services[wordsConfig.TagGeneratorName]();
    }
}
