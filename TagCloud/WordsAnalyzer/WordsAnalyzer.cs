using System.Collections.Generic;
using System.Linq;
using TagCloud.WordsAnalyzer.WordFilters;
using TagCloud.WordsAnalyzer.WordNormalizer;

namespace TagCloud.WordsAnalyzer
{
    public class WordsAnalyzer : IWordsAnalyzer
    {
        private IWordNormalizer normalizer;
        private HashSet<IWordFilter> filters;
        
        public WordsAnalyzer(IWordNormalizer normalizer, params IWordFilter[] filters)
        {
            this.normalizer = normalizer;
            this.filters = filters.ToHashSet();
        }
        
        public HashSet<TagInfo> GetTags(IReadOnlyCollection <string> words)
        {
            var wordsCount = GetWordsCounts(words);
            if (wordsCount.Count == 0)
                return new HashSet<TagInfo>();
            
            var minCount = wordsCount.Values.ToList().Min();
            var maxCount = wordsCount.Values.ToList().Max();
            
            var tags = wordsCount
                .Select(wordToCount => new TagInfo(
                    wordToCount.Key, 
                    GetWeight(wordToCount.Value, minCount, maxCount)))
                .ToHashSet();
            return tags;
        }

        private Dictionary<string, int> GetWordsCounts(IReadOnlyCollection <string> words)
        {
            return words.Select(word => normalizer.Normalize(word))
                    .Where(word => !filters.Any(f => f.ShouldExclude(word)))
                    .GroupBy(word => word)
                    .ToDictionary(group => group.Key, group => group.Count());
        }

        private static double GetWeight(int value, int minValue, int maxValue)
        {
            return minValue != maxValue ? (double) (value - minValue) / (maxValue - minValue) : 1;
        }
    }
}