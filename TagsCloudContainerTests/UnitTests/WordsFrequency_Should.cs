using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainerTests.UnitTests
{
    public class WordsFrequencyShould
    {
        private WordsFrequency _sut;
        private ITextConverter _textConverter;
        private IWordsFilter _wordsFilter;
        private IWordsFilter _speechPartFilter;

        [SetUp]
        public void SetUp()
        {
            _textConverter = A.Fake<ITextConverter>();
            _wordsFilter = A.Fake<IWordsFilter>();
            _speechPartFilter = A.Fake<IWordsFilter>();
            _sut = new WordsFrequency(_textConverter, new List<IWordsFilter> {_speechPartFilter, _wordsFilter});
        }

        [Test]
        public void GetWordsFrequency_ThrowException_WhenStringIsNull()
        {
            var act = new Action(() => _sut.GetWordsFrequency(null));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetWordsFrequency_NotBeCalledGetInterestingWordsForEachFilters_WhenStringIsNull()
        {
            try
            {
                _sut.GetWordsFrequency(null);
            }
            catch
            {
                // ignored
            }

            A.CallTo(() => _wordsFilter.GetInterestingWords(A<string[]>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _speechPartFilter.GetInterestingWords(A<string[]>.Ignored)).MustNotHaveHappened();
        }

        [TestCase("")]
        [TestCase("word")]
        public void GetWordsFrequency_NotException_WhenStringIsNotNull(string text)
        {
            var act = new Action(() => _sut.GetWordsFrequency(text));

            act.Should().NotThrow<Exception>();
        }

        [TestCase("")]
        [TestCase("words")]
        public void GetWordsFrequency_BeCalledGetInterestingWordsOnceForEachFilters_WhenStringIsNotNull(string text)
        {
            _sut.GetWordsFrequency(text);

            A.CallTo(() => _wordsFilter.GetInterestingWords(A<string[]>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _speechPartFilter.GetInterestingWords(A<string[]>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [TestCase("дом стол кот")]
        public void GetWordsFrequency_SameNumberWordKeys_WhenDifferentWords(string text)
        {
            var words = text.Split(' ');
            A.CallTo(() => _wordsFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);
            A.CallTo(() => _speechPartFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);

            var act = _sut.GetWordsFrequency(text);

            act.Should().HaveCount(words.Length);
        }

        [TestCase("вода игра вода")]
        public void GetWordsFrequency_LessNumberWordsKeys_WhenExistsSameWords(string text)
        {
            var words = text.Split(' ');
            A.CallTo(() => _wordsFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);
            A.CallTo(() => _speechPartFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);

            var act = _sut.GetWordsFrequency(text);

            act.Should().HaveCount(words.Distinct().Count());
        }

        [Test]
        public void GetWordsFrequency_WordsFrequency_WhenDifferentWords()
        {
            var text = "кот лодка вода";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);
            A.CallTo(() => _speechPartFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);

            var act = _sut.GetWordsFrequency(text);

            act.Should().BeEquivalentTo(new Dictionary<string, int> {[words[0]] = 1, [words[1]] = 1, [words[2]] = 1});
        }

        [Test]
        public void GetWordsFrequency_WordsFrequency_WhenExistsSameWords()
        {
            var text = "кот кот лодка вода";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);
            A.CallTo(() => _speechPartFilter.GetInterestingWords(A<string[]>.Ignored)).Returns(words);

            var act = _sut.GetWordsFrequency(text);

            act.Should().BeEquivalentTo(new Dictionary<string, int> {[words[0]] = 2, [words[2]] = 1, [words[3]] = 1});
        }
    }
}