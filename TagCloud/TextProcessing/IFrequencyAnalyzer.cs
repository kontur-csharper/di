using System.Collections.Generic;

namespace TagCloud.TextProcessing
{
    public interface IFrequencyAnalyzer
    {
        Dictionary<string, double> GetFrequencyDictionary(string fileName);
    }
}