using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TagsCloud.WordSelectors;

namespace TagsCloud.WordReaders
{
    public class RegexWordReader : IWordReader
    {
        private readonly string filePath;
        private readonly IWordSelector selector;

        public RegexWordReader(string filePath, IWordSelector selector)
        {
            this.filePath = filePath;
            this.selector = selector;
        }

        //Через Regex удобно тестировать разные тексты.
        public IEnumerable<string> ReadWords()
        {
            try
            {
                using var reader = new StreamReader(filePath);
                var words = Regex.Split(reader.ReadToEnd(), "\\W+");
                return selector.TakeSelectedWords(words);
            }
            catch
            {
                Console.WriteLine($"Failed to read {filePath}");
                return null;
            }
        }
    }
}