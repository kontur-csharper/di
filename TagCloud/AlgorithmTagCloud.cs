using TagCloud.CloudLayoters;
using TagCloud.WordsMetrics;
using TagCloud.TextConverters.TextProcessors;

namespace TagCloud
{
    public static class AlgorithmTagCloud
    {
        internal static TagCloud GetTagCloud(string text, ICloudLayoter layoter, 
            ITextProcessor textProcessor, IWordsMetric wordsMetric)
        {
            var metric = wordsMetric.GetMetric(textProcessor.GetLiterals(text));
            return new TagCloud(metric, layoter);
        }
    }
}
