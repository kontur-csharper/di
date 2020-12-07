using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;

namespace TagsCloudTests
{
    [TestFixture]
    internal class WordNormalizerTests
    {
        private readonly WordNormalizer normalizer = new WordNormalizer();

        [TestCase("Word", "word")]
        [TestCase("SECOND", "second")]
        [TestCase("", "")]
        [TestCase("AbC541", "abc541")]
        [TestCase("kU!@Hg_rug", "ku!@hg_rug")]
        public void WordNormalizer_ShouldMakeWordsLower(string word, string expectedResult)
        {
            var result = normalizer.Normalize(word);
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}