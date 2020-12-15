using System.Collections.Generic;
using System.Linq;
using TagCloud.TextConverters.WordExcluders;

namespace TagCloud.TextConverters.TextProcessors
{
    public class ParagraphTextProcessor : ITextProcessor
    {
        private readonly IWordExcluder excluder = new WordsExcluder();
        public IEnumerable<string> GetLiterals(string text) =>
            text
            .Split('\n')
            .Where(s => s != string.Empty)
            .Select(s => s.ToLower())
            .Where(s => !excluder.MustBeExclude(s));
    }
}
