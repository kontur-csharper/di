using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordsCleaners;

namespace TagsCloudVisualization_Should
{
    public class BoringWordsCleanerShould
    {
        [Test]
        public void CleanWords_NotContainsBoringWords_TextWithBoringWords()
        {
            var boringWords = new List<string>
            {
                "в", "без", "до", "для", "за", "через", "над", "по", "из", "у", "около",
                "под", "о", "про", "на", "к", "перед", "при", "с", "между"
            }.ToHashSet();

            var words = new List<string> {"он", "пошел", "на", "встречу"};
            var expectedWords = new List<string> {"он", "пошел", "встречу"};
            var cleaner = new BoringWordsCleaner();
            cleaner.AddBoringWords(boringWords);

            var actualWords = cleaner.CleanWords(words);

            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}