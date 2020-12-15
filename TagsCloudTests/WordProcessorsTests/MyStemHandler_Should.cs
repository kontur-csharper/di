using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudCreating.Core.WordProcessors;

namespace TagsCloudTests.WordProcessorsTests
{
    [TestFixture]
    public class MyStemHandler_Should
    {
        [TestCase("тест")]
        [TestCase("ТЕСТ")]
        [TestCase("Тест")]
        [TestCase("ТеСт")]
        public void GetWordsWithPartsOfSpeech_InsertWords_ReturnsWordsInLowerCase(string testWord) =>
            GetProcessedWords(testWord)
                .First()
                .word
                .Should()
                .BeEquivalentTo("тест");

        [Test]
        public void GetWordsWithPartsOfSpeech_InsertNoun_ReturnsCorrectPartOfSpeech() =>
            GetProcessedWords("Тумба").First().speechPart.Should().Be("S");

        [Test]
        public void GetWordsWithPartsOfSpeech_InsertWords_ReturnsCorrectPartsOfSpeech()
        {
            string[] expectedPartsOfSpeech = {"S", "A", "ADV", "INTJ", "PART"};

            GetProcessedWords("Стол", "Красивый", "ГрУбо", "ах", "НЕ")
                .Select(p => p.speechPart)
                .Should()
                .BeEquivalentTo(expectedPartsOfSpeech);
        }

        private static IEnumerable<(string word, string speechPart)> GetProcessedWords(params string[] words) =>
            MyStemHandler.GetWordsWithPartsOfSpeech(words);
    }
}