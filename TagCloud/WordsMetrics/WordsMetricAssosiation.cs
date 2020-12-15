using System.Collections.Generic;

namespace TagCloud.WordsMetrics
{
    public static class WordsMetricAssosiation
    {
        public const string count = "count";
        private static readonly Dictionary<string, IWordsMetric> metrics =
            new Dictionary<string, IWordsMetric>
            {
                [count] = new CountWordMetric()
            };

        public static IWordsMetric GetMetric(string name) => 
            metrics.TryGetValue(name, out var metric) ? metric : null;
    }
}
