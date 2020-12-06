using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCloudGenerator
{
    public abstract class GeneratorAlgorithms
    {
        public static Func<IEnumerable<WordFrequency>, IEnumerable<GraphicString>> Exponential => ExponentialAlgorithm;

        public static Func<IEnumerable<WordFrequency>, IEnumerable<GraphicString>> Proportional =>
            ProportionalAlgorithm;

        private static IEnumerable<GraphicString> ExponentialAlgorithm(IEnumerable<WordFrequency> wordFrequencies)
        {
            var fontSize = 400f;

            foreach (var wordFrequency in wordFrequencies)
            {
                yield return new GraphicString(wordFrequency.Word, Math.Max(75, fontSize));
                fontSize /= 1.1f;
            }
        }

        private static IEnumerable<GraphicString> ProportionalAlgorithm(IEnumerable<WordFrequency> wordFrequencies)
        {
            var frequencies = wordFrequencies.ToArray();
            var freqSum = frequencies.Sum(word => word.Frequency);
            var recalculatedFrequencies =
                frequencies.Select(word => new WordFrequency(word.Word, word.Frequency * 2 / freqSum));

            var maxFontSize = 1500;

            foreach (var word in recalculatedFrequencies)
                yield return new GraphicString(word.Word, Math.Max(40, word.Frequency * maxFontSize));
        }
    }
}