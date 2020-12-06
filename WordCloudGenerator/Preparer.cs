using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCloudGenerator
{
    public class Preparer : IPreparer
    {
        private readonly Func<string, bool> filter;
        private readonly HashSet<string> wordsToSkip;

        public Preparer(IEnumerable<string> wordsToSkip, Func<string, bool> filter)
        {
            this.filter = filter;
            this.wordsToSkip = wordsToSkip == null ? new HashSet<string>() : wordsToSkip.ToHashSet();
        }

        public Preparer(IEnumerable<string> wordsToSkip)
        {
            this.wordsToSkip = wordsToSkip == null ? new HashSet<string>() : wordsToSkip.ToHashSet();
            filter = s => true;
        }

        public IEnumerable<WordFrequency> CreateWordFreqList(string inputText, int maxWordCount = 100)
        {
            var text = inputText.ToLower();
            var wordsInText = Regex.Matches(text, @"\w+")
                .Select(match => match.Value)
                .Where(word => !wordsToSkip.Contains(word)).ToArray();

            return wordsInText.Distinct()
                .Where(filter)
                .Select(word => new WordFrequency(word,
                    (float) wordsInText.Count(el => el == word) / wordsInText.Length))
                .OrderByDescending(word => word.Frequency)
                .Take(maxWordCount);
        }
    }
}