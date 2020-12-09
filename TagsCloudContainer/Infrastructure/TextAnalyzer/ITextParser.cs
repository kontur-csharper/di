using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    public interface ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines);
    }
}