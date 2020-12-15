using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Core.WordProcessors;

namespace TagsCloudTests.WordProcessorsTests
{
    [TestFixture]
    public class WordConverter_Should
    {
        [TestCase(3, "word", "word", "word")]
        [TestCase(2, "word", "wor", "band", "testWord", "word")]
        public void ConvertToTags_SomeWords_ReturnsTagsWithCorrectFrequency(int frequency, params string[] words) =>
            new WordConverter(new TagsSettings()).ConvertToTags(words)
                .Where(tag => words.First() == tag.Word)
                .Select(tag => tag.Frequency)
                .Should()
                .BeEquivalentTo(frequency);
    }
}