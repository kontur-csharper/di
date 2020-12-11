using System.Collections.Generic;
using System.Linq;

namespace TagCloud.TextFileParser
{
    public class ToLowerCaseProcessor : IWordsHandler
    {
        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}