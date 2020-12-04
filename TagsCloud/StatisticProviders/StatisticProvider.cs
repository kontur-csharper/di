using System.Collections.Generic;
using System.Linq;
using TagsCloud.BoringWordsDetectors;

namespace TagsCloud.StatisticProviders
{
    public class StatisticProvider : IStatisticProvider
    {
        private readonly IBoringWordsDetector boringWordsDetector;
        
        public StatisticProvider(IBoringWordsDetector boringWordsDetector)
        {
            this.boringWordsDetector = boringWordsDetector;
        }

        public Dictionary<string, int> GetWordStatistics(IEnumerable<string> words)
        {
            var result = new Dictionary<string, int>();
            
            foreach (var word in PrepareWords(words))
            {
                if (!result.ContainsKey(word))
                    result[word] = 0;
                result[word]++;
            }

            return result;
        }

        private IEnumerable<string> PrepareWords(IEnumerable<string> words) =>
            words.Select(w => w.ToLower()).Where(w => !boringWordsDetector.IsBoring(w));
    }
}