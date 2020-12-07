using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.Text.Formatting
{
    public class BiggerAtCenterFontSizeSource : IFontSizeSource
    {
        private const int MinFontSize = 10;
        private const int Step = 3;

        public FontSizeSourceType Type => FontSizeSourceType.FrequentIsBigger;

        public IDictionary<string, float> GetFontSizesForAll(Dictionary<string, int> allWords)
        {
            var log = (int) Math.Floor(Math.Log2(allWords.Count));
            var sizes = ToStack(
                Enumerable.Range(1, log)
                    .Select(i => (int) Math.Pow(2, i))
                    .Select((c, i) => (Count: c, Size: log * Step - i * Step + MinFontSize))
                    .Reverse()
            );

            var currentSize = sizes.Pop();
            var currentSizeUses = 0;
            var result = new Dictionary<string, float>();
            foreach (var word in allWords)
            {
                result.Add(word.Key, currentSize.Size);
                if (currentSizeUses++ >= currentSize.Count)
                {
                    currentSize = sizes.Pop();
                    currentSizeUses = 0;
                }
            }

            return result;
        }

        private static Stack<T> ToStack<T>(IEnumerable<T> source) => new Stack<T>(source);
    }
}