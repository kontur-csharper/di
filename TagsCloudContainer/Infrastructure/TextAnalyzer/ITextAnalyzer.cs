using System.Collections.Generic;

namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    public interface ITextAnalyzer
    {
        public Dictionary<string, double> GenerateFrequencyDictionary(IEnumerable<string> lines);
    }
}