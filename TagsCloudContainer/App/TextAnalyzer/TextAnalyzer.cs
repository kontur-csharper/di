using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.Infrastructure.TextAnalyzer;

namespace TagsCloudContainer.App.TextAnalyzer
{
    public class TextAnalyzer : ITextAnalyzer
    {
        private readonly ITextParser textParser;
        private readonly IEnumerable<IWordFilter> wordFilters;
        private readonly IEnumerable<IWordNormalizer> wordNormalizers;

        public TextAnalyzer(ITextParser textParser,
            IEnumerable<IWordNormalizer> wordNormalizers, IEnumerable<IWordFilter> wordFilters)
        {
            this.textParser = textParser;
            this.wordNormalizers = wordNormalizers;
            this.wordFilters = wordFilters;
        }

        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines)
        {
            var notBoringWords = textParser.GetWords(lines)
                .NormalizeWords(wordNormalizers)
                .FilterOutBoringWords(wordFilters);
            var notBoringWordsCount = notBoringWords.Count();
            return notBoringWords
                .GroupBy(word => word)
                .ToDictionary(group => group.Key,
                    group => (double) group.Count() / notBoringWordsCount);
        }
    }
}