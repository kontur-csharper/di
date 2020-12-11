using System.Collections.Generic;

namespace TagCloud.TextFileParser
{
    public interface IWordsHandler
    {
        public IEnumerable<string> ProcessWords(IEnumerable<string> words);
    }
}