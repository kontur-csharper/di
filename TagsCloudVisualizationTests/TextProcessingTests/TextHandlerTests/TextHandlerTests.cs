using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.TextProcessing.TextHandler;
using TagsCloudVisualization.Words;
using TagsCloudVisualization.WordsProcessing.WordsFilters;
using TagsCloudVisualization.WordsProcessing.WordsWeighers;

namespace TagsCloudVisualizationTests.TextProcessingTests.TextHandlerTests
{
    public class TextHandlerTests
    {
        [SetUp]
        public void SetUp()
        {
            var filters = new []{new ForbiddenWordFilter(new WordsSettings())};
            textHandler = new FrequencyTextHandler(filters, new FrequencyWordWeigher());
        }

        private FrequencyTextHandler textHandler;

        [Test]
        public void GetWordsFrequency_ContainWordsFromInput_WhenInputContain3NonRepeatingWords()
        {
            var expected = new List<Word>
            {
                new Word("hello", 1),
                new Word("world", 1),
                new Word("annd", 1),
                new Word("arina", 1)
            };

            var result = textHandler.GetHandledWords("hello world annd Arina");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_ContainWordsFromInput_WhenInputContain4RepeatingWords()
        {
            var expected = new List<Word> {new Word("hello", 4)};

            var result = textHandler.GetHandledWords("hello hello hello hello");

            result.Should().BeEquivalentTo(expected);
        }


        [Test]
        public void GetWordsFrequency_ContainWordsInLowerCase_WhenInputContainsWordInUpperCase()
        {
            var expected = new List<Word> {new Word("hello", 1)};

            var result = textHandler.GetHandledWords("HELLO");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_NotContainForbiddenSigns()
        {
            var expected = new List<Word> {new Word("hello", 1)};

            var result = textHandler.GetHandledWords("Hello!,!.+ 21!");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_OrderedByFrequenceDescending()
        {
            var expected = new List<Word>
            {
                new Word("hello", 3),
                new Word("world", 2),
                new Word("good", 1)
            };

            var result = textHandler.GetHandledWords("Hello hello hello good world world");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_ExcludedShortWords_WhenInputContains1ShortWord()
        {
            var expected = new List<Word>
            {
                new Word("hello", 1),
                new Word("world", 1)
            };

            var textHandler2 = new FrequencyTextHandler(
                new[] {new LengthFilter(new WordsSettings {MinLength = 4})},
                new FrequencyWordWeigher());
            
            var result = textHandler2.GetHandledWords("Hello all world");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_ExcludedForbiddenWords_WhenExistsOneForbiddenWord()
        {
            var expected = new List<Word>
            {
                new Word("some", 1),
                new Word("text", 1)
            };
            var textHandler2 = new FrequencyTextHandler(
                new[] {new ForbiddenWordFilter(new WordsSettings {ForbiddenWords = new[] {"beautiful"}})},
                new FrequencyWordWeigher());

            var result = textHandler2.GetHandledWords(
                "Some beautiful text");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_ExcludedForbiddenWords_WhenExistsMoreThanOneForbiddenWord()
        {
            var expected = new List<Word>
            {
                new Word("text", 1)
            };
            var textHandler2 = new FrequencyTextHandler(
                new[] {new ForbiddenWordFilter(new WordsSettings {ForbiddenWords = new[] {"some", "beautiful"}})},
                new FrequencyWordWeigher());

            var result = textHandler2.GetHandledWords(
                "Some beautiful text");

            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsFrequency_ExcludedAllWords_WhenForbiddenWordsCountEqualTextWordsCount()
        {
            var expected = new List<Word>();
            var textHandler2 = new FrequencyTextHandler(
                new[]
                {
                    new ForbiddenWordFilter(new WordsSettings {ForbiddenWords = new[] {"some", "beautiful", "text"}})
                },
                new FrequencyWordWeigher());

            var result = textHandler2.GetHandledWords(
                "Some beautiful text");

            result.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GetWordsFrequency_ExcludedFilteredWords_WhenExistsTwoFilters()
        {
            var expected = new List<Word>{new Word("beautiful", 1)};
            var wordSettings = new WordsSettings {ForbiddenWords = new[] {"some", "text"}, MinLength = 4};
            var textHandler2 = new FrequencyTextHandler(
                new IWordFilter[]
                {
                    new ForbiddenWordFilter(wordSettings),
                    new LengthFilter(wordSettings)
                },
                new FrequencyWordWeigher());

            var result = textHandler2.GetHandledWords("Some lin beautiful text");

            result.Should().BeEquivalentTo(expected);
        }
    }
}