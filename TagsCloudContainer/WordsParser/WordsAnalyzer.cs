using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordsParser
{
    public class WordsAnalyzer : IWordsAnalyzer
    {
        private readonly IFilter filter;
        private readonly IWordReader wordReader;

        public WordsAnalyzer(IFilter filter, IWordReader wordReader)
        {
            this.filter = filter;
            this.wordReader = wordReader;
        }

        public Dictionary<string, int> AnalyzeWords()
        {
            var wordsCount = new Dictionary<string, int>();

            var words = new List<string>();

            while (true)
            {
                var word = wordReader.ReadWord();
                if (word is null)
                    break;
                words.Add(word.NormalizeWord());
            }

            var filteredWords = filter.RemoveBoringWords(words.ToHashSet());
            foreach (var word in words)
            {
                if (filteredWords.Contains(word))
                    wordsCount.SetOrUpdate(word);
            }

            return wordsCount;
        }
    }
}