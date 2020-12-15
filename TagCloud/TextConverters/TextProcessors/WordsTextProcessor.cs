using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TagCloud.TextConverters.WordExcluders;

namespace TagCloud.TextConverters.TextProcessors
{
    public class WordsTextProcessor : ITextProcessor
    {
        private readonly IWordExcluder excluder = new WordsExcluder();
        public IEnumerable<string> GetLiterals(string text) =>
            Regex.Split(text, @"\W+")
            .Where(s => s != string.Empty)
            .Select(s => s.ToLower())
            .Where(s => !excluder.MustBeExclude(s));
    }
}
