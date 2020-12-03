using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.WordConverters;
using TagCloud.Core.WordsFilters;
using TagCloud.Extensions;

namespace TagCloud.Core.WordsProcessors
{
    public class WordsProcessor : IWordsProcessor
    {
        private readonly List<IWordFilter> filters;
        private readonly List<IWordConverter> converters;

        public WordsProcessor(IEnumerable<IWordFilter> filters,
            IEnumerable<IWordConverter> converters)
        {
            this.converters = converters.ToList();
            this.filters = filters.ToList();
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words
                .Where(IsValid)
                .Select(Convert)
                .MostFrequent()
                .Select(e => e.item);
        }

        public IEnumerable<string> Process(IEnumerable<string> words, int amountToTake)
        {
            return Process(words)
                .Take(amountToTake);
        }

        private string Convert(string word)
        {
            return converters.Aggregate(word,
                (current, wordHandler) => wordHandler.Convert(current));
        }

        private bool IsValid(string word)
        {
            return filters.All(filter => filter.IsValid(word));
        }
    }
}