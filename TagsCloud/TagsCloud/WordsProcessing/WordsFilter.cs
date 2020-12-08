using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloud.WordsProcessing
{
    public class WordsFilter : IWordsFilter
    {
        private HashSet<string> wordsToIgnore;
        private string regexWordPattern;
        public WordsFilter(HashSet<string> wordsToIgnore, string regexWordPattern = "^\\w+$")
        {
            this.wordsToIgnore = wordsToIgnore;
            this.regexWordPattern = regexWordPattern;
        }

        public IEnumerable<string> GetCorrectWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                if (!Regex.IsMatch(word, regexWordPattern))
                    throw new FormatException("Входной файл должен содержать только одно слово в строке");
                if(!wordsToIgnore.Contains(word))
                    yield return word.ToLower();
            }
        }
    }
}
