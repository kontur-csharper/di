using System.Collections.Generic;

namespace TagCloud.WordsProvider
{
    public class CircularWordsProvider : IWordsProvider
    {
        public const int WordsCount = 100;
        public List<string> words = new List<string> {"word", "another", "123", "VeryVeryLongWord", "word"};

        public IEnumerable<string> GetWords()
        {
            var i = 0;
            while (i < WordsCount)
            {
                yield return words[i % words.Count];
                i++;
            }
        }
    }
}