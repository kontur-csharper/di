using System;
using System.Collections.Generic;
using System.IO;
using TagsCloud.WordSelectors;

namespace TagsCloud.WordReaders
{
    public class ByLineWordReader : IWordReader
    {
        private readonly string filePath;
        private readonly IWordSelector selector;
        private readonly string[] separator = {Environment.NewLine};

        public ByLineWordReader(string filePath, IWordSelector selector)
        {
            this.filePath = filePath;
            this.selector = selector;
        }

        public IEnumerable<string> ReadWords()
        {
            try
            {
                using var reader = new StreamReader(filePath);
                var words = reader.ReadToEnd().Split(separator, StringSplitOptions.None);
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