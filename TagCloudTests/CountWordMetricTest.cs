using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using TagCloud.WordsMetrics;
using FluentAssertions;

namespace TagCloudTests
{
    [TestFixture]
    internal class CountWordMetricTest
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("a", "aa")]
        [TestCase("a", "aa", "a")]
        [TestCase("a", "b", "a", "c", "b", "a")]
        public void CountWordMetric_ShouldAssosiationWordAndWordPositionsCount(params string[] words)
        {
            var result = new CountWordMetric().GetMetric(words);
            foreach (var word in words)
                result.Should().Contain(new KeyValuePair<string, double>(word, words.Where(w => w == word).Count()));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("a", "aa")]
        [TestCase("a", "aa", "a")]
        [TestCase("a", "b", "a", "c", "b", "a")]
        public void CountWordMetrix_ShouldContainOnlyWordsFromCollection(params string[] words)
        {
            var result = new CountWordMetric().GetMetric(words);
            result.Keys.Should().BeEquivalentTo(words.ToHashSet());
        }
    }
}
