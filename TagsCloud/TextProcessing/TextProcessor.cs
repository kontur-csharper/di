using System.Collections.Generic;
using System.Linq;
using TagsCloud.Factory;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.TextReaders;

namespace TagsCloud.TextProcessing
{
    public class TextProcessor
    {
        private readonly IServiceFactory<IWordsReader> readersFactory;
        private readonly IServiceFactory<ITextFilter> filtersFactory;
        private readonly IServiceFactory<IWordConverter> convertersFactory;

        public TextProcessor(IServiceFactory<IWordsReader> readers, IServiceFactory<ITextFilter> filters,
            IServiceFactory<IWordConverter> converters)
        {
            readersFactory = readers;
            filtersFactory = filters;
            convertersFactory = converters;
        }

        public IEnumerable<WordInfo> ReadFromFile(string path)
        {
            var words = readersFactory.Create().ReadWords(path);

            var filter = filtersFactory.Create();
            var filteredWords = words.Where(word => filter.CanTake(word));

            var converter = convertersFactory.Create();
            var convertedWords = filteredWords.Select(word => converter.Convert(word));

            return convertedWords.GroupBy(w => w).Select(g => new WordInfo(g.Key, g.Count()));
        }
    }
}
