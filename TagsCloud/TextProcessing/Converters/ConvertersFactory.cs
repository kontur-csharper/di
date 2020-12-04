using System.Linq;
using TagsCloud.Factory;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TextProcessing.Converters
{
    public class ConvertersFactory : ServiceFactory<IWordConverter>
    {
        private readonly WordConfig wordsConfig;

        public ConvertersFactory(WordConfig wordsConfig)
        {
            this.wordsConfig = wordsConfig;
        }

        public override IWordConverter Create()
        {
            var converterNames = wordsConfig.ConvertersNames;
            return new CompositeConverter(converterNames.Select(name => services[name]()).ToArray());
        }

        private class CompositeConverter : IWordConverter
        {
            private readonly IWordConverter[] converters;

            public CompositeConverter(IWordConverter[] converters)
            {
                this.converters = converters;
            }

            public string Convert(string word) =>
                converters.Aggregate(word, (current, converter) => converter.Convert(current));
        }
    }
}
