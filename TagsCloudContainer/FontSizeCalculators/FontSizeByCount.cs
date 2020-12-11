using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class FontSizeByCount : IFontSizeCalculator
    {
        private const float maxFontSize = 25;
        private const float minFontSize = 10;
        public IEnumerable<WordWithFont> CalculateFontSize(IEnumerable<string> words, FontFamily fontFamily)
        {
            var wordCounts = CountWords(words);
            var maxCount = wordCounts.Values.Max();
            var minCount = wordCounts.Values.Min();
            foreach (var wordCount in wordCounts)
            {
                var fontSize = maxFontSize * (wordCount.Value - minCount) / (maxCount - minCount) + minFontSize;
                yield return new WordWithFont(wordCount.Key, new Font(fontFamily, fontSize));
            }
        }

        private Dictionary<string, int> CountWords(IEnumerable<string> words)
        {
            var wordCounts = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (wordCounts.ContainsKey(word))
                    wordCounts[word]++;
                else
                    wordCounts[word] = 0;
            }

            return wordCounts;
        }
    }
}
