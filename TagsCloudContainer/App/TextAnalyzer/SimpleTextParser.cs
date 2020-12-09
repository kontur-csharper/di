using System.Collections.Generic;
using TagsCloudContainer.Infrastructure.TextAnalyzer;

namespace TagsCloudContainer.App.TextAnalyzer
{
    internal class SimpleTextParser : ITextParser
    {
        public IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            return lines;
        }
    }
}