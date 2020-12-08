using System.Collections.Generic;
using System.IO;

namespace TagsCloud.WordsProcessing
{
    public class WordsFrequencyParser : IWordsFrequencyParser
    {
        private IWordsFilter filter;
        public WordsFrequencyParser(IWordsFilter filter)
        {
            this.filter = filter;
        }

        public Dictionary<string, int> ParseWordsFrequencyFromFile(string fileName)
        {
            var frequencies = new Dictionary<string, int>();
            var words = filter.GetCorrectWords(File.ReadLines(fileName));
            foreach (var word in words)
                if (frequencies.ContainsKey(word))
                    frequencies[word]++;
                else
                    frequencies[word] = 1;

            return frequencies;
        }
    }
}