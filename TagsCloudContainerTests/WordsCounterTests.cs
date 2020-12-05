using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using WeCantSpell.Hunspell;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class WordsCounterTests
    {
        private MorphologicalWordsCounter counter;
        private WordList wordList;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wordList = WordList.CreateFromFiles(
                "../../../../TagsCloudContainerConsole/Dictionaries/English (American).dic",
                "../../../../TagsCloudContainerConsole/Dictionaries/English (American).aff");
        }

        [SetUp]
        public void SetUp()
        {
            counter = new MorphologicalWordsCounter(wordList);
        }

        [Test]
        public void CountWords_IfGetEmptyArrayAsArgument_ReturnEmptyDict()
        {
            var emptyArray = new string[0];

            counter.CountWords(emptyArray).Should().BeEmpty();
        }

        [Test]
        public void CountWords_CorrectCountOneWord()
        {
            var words = new[] {"a", "a"};

            counter.CountWords(words).Should()
                .BeEquivalentTo(new Dictionary<string, int> {{"a", 2}});
        }

        [Test]
        public void CountWords_CorrectCountSeveralWords()
        {
            var words = new[] {"a", "a", "b"};

            counter.CountWords(words).Should()
                .BeEquivalentTo(new Dictionary<string, int> {{"a", 2}, {"b", 1}});
        }

        [Test]
        public void CountWords_IgnoreCaseOfWords()
        {
            var words = new[] {"a", "A"};

            counter.CountWords(words).Should()
                .BeEquivalentTo(new Dictionary<string, int> {{"a", 2}});
        }

        [Test]
        public void CountWords_LowercaseStringsInDict()
        {
            var words = new[] {"A"};

            counter.CountWords(words).Should()
                .BeEquivalentTo(new Dictionary<string, int> {{"a", 1}});
        }

        [TestCaseSource(nameof(CountWords_DoesNotCountWordsOfExcludedPartsOfSpeechCases))]
        public void CountWords_DoesNotCountWordsOfExcludedPartsOfSpeech(PartOfSpeech[] excludedPartsOfSpeech,
            string[] words, Dictionary<string, int> expectedResult)
        {
            var counter = new MorphologicalWordsCounter(wordList, excludedPartsOfSpeech);

            counter.CountWords(words).Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void CountWords_GetRootOfWord()
        {
            var words = new[] {"dislike"};
            
            counter.CountWords(words).Should().BeEquivalentTo(
                new Dictionary<string, int> {{"like", 1}});
        }

        [Test]
        public void CountWords_IfPronounIsExcludedPartOfSpeech_ExcludePronounInСontractedForm()
        {
            var counter = new MorphologicalWordsCounter(wordList, new[] {PartOfSpeech.Pronoun});
            var words = new[] {"we'll", "you're", "a"};

            counter.CountWords(words).Should().BeEquivalentTo(
                new Dictionary<string, int> {{"a", 1}});
        }

        private static object[] CountWords_DoesNotCountWordsOfExcludedPartsOfSpeechCases =
        {
            new TestCaseData(
                    new[] {PartOfSpeech.Pronoun},
                    new[] {"we", "he", "a"},
                    new Dictionary<string, int> {{"a", 1}})
                .SetName("ExcludePronoun"),
            
            new TestCaseData(
                    new[] {PartOfSpeech.Conjunction},
                    new[] {"and", "a"},
                    new Dictionary<string, int> {{"a", 1}})
                .SetName("ExcludeConjunction"),
            
            new TestCaseData(
                    new[] {PartOfSpeech.Preposition},
                    new[] {"to", "a"},
                    new Dictionary<string, int> {{"a", 1}})
                .SetName("ExcludePrepositinos"),
            
            new TestCaseData(
                    new[] {PartOfSpeech.Determiner},
                    new[] {"the", "b"},
                    new Dictionary<string, int> {{"b", 1}})
                .SetName("ExcludeDeterminers"),
            
            new TestCaseData(
                    new[] {PartOfSpeech.Conjunction, PartOfSpeech.Pronoun},
                    new[] {"and", "it", "a"},
                    new Dictionary<string, int> {{"a", 1}})
                .SetName("ExcludeConjunctionAndPronoun"),
        };
    }
}