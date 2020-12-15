using System.Collections.Generic;

namespace TagCloud.TextConverters.TextProcessors
{
    public interface ITextProcessor
    {
        public IEnumerable<string> GetLiterals(string text);
    }
}
