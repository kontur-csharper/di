using NUnit.Framework;
using TagCloud.TextConverters.TextProcessors;
using System.Linq;
using FluentAssertions;
using TagCloud.TextConverters.WordExcluders;

namespace TagCloudTests
{
    [TestFixture]
    internal class TextProcessorTest
    {
        [TestCase("m", ExpectedResult = new[] { "m" })]
        [TestCase("m\nm", ExpectedResult = new[] { "m", "m"})]
        [TestCase("text\nprocessor\ntest", ExpectedResult = new[] { "text", "processor", "test" })]
        public string[] TextProcess_ShouldSplitTextNewLine(string text)
        {
            return new ParagraphTextProcessor().GetLiterals(text).ToArray();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("\n")]
        [TestCase("m\nm\n")]
        [TestCase("\n\n\nm\n")]
        [TestCase("m\nm\nm")]
        public void TextProcessor_ShouldExludeEmptyString(string text)
        {
            var result = new ParagraphTextProcessor().GetLiterals(text);
            result.Should().NotContain(string.Empty);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("Misha")]
        [TestCase("M\n\nnn\nMM")]
        [TestCase("TeXt\nProCeSSOR\nSHOULD\nwords\tO\nLoweR\ncASe")]
        public void TextProcessor_ShouldWordsToLowerCase(string text)
        {
            var result = new ParagraphTextProcessor().GetLiterals(text);
            foreach (var word in result)
                Assert.AreEqual(word.ToLower(), word);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("a")]
        [TestCase("an")]
        [TestCase("i\nam\nbetman")]
        [TestCase("hi\nis\nnot\nA\nbetman")]
        [TestCase("text\nwothout\nburing\nwords")]
        public void TextProcessor_ShouldExludeBurindWords(string text)
        {
            var result = new ParagraphTextProcessor().GetLiterals(text);
            var wordsExcluder = new WordsExcluder();
            result.Should().NotContain(p => wordsExcluder.MustBeExclude(p));
        }
    }
}
