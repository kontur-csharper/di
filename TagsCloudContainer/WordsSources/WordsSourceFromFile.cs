using System.Collections.Generic;
using System.IO;

namespace TagsCloudContainer
{
    public class WordsSourceFromFile : IWordsSource
    {
        public readonly string FileName;

        public WordsSourceFromFile(string fileName)
        {
            FileName = fileName;
        }

        public IEnumerable<(string word, int count)> GetWords()
            => new WordsSourceFromText(File.ReadAllText(FileName)).GetWords();
    }
}