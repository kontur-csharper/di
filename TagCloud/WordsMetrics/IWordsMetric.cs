using System.Collections.Generic;

namespace TagCloud.WordsMetrics
{
    public interface IWordsMetric
    {
        public Dictionary<string, double> GetMetric(IEnumerable<string> words);
    }
}
