using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainerTests.UnitTests
{
    public class WordTagsLayouterShould
    {
        private WordTagsLayouter _sut;
        private IWordsFrequency _wordsFrequency;
        private ICloudLayouter _cloudLayouter;
        private IWordMeasurer _wordMeasurer;
        private static readonly Font _font = new Font("arial", 5);

        [SetUp]
        public void SetUp()
        {
            _wordsFrequency = A.Fake<IWordsFrequency>();
            _cloudLayouter = A.Fake<ICloudLayouter>();
            _wordMeasurer = A.Fake<IWordMeasurer>();
            _sut = new WordTagsLayouter(_wordsFrequency, _cloudLayouter, _wordMeasurer, _font);
        }

        [Test]
        public void GetWordTagsAndCloudRadius_ThrowException_WhenStringIsNotNull()
        {
            var act = new Action(() => _sut.GetWordTagsAndCloudRadius(null));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetWordTagsAndCloudRadius_BeNotCalledAnyMethodDependencies_WhenStringIsNull()
        {
            try
            {
                _sut.GetWordTagsAndCloudRadius(null);
            }
            catch
            {
                // ignored
            }

            A.CallTo(() => _wordsFrequency.GetWordsFrequency(A<string>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _wordMeasurer.GetWordSize(A<string>.Ignored, A<Font>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _cloudLayouter.PutNextRectangle(A<Size>.Ignored)).MustNotHaveHappened();
        }

        [TestCase("")]
        [TestCase("игра")]
        public void GetWordTagsAndCloudRadius_BeCalledGetWordsFrequencyOnce_WhenStringIsNotNull(string text)
        {
            _sut.GetWordTagsAndCloudRadius(text);

            A.CallTo(() => _wordsFrequency.GetWordsFrequency(A<string>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void GetWordTagsAndCloudRadius_BeCalledGetWordSizeCertainNumber_WhenStringIsNotNull()
        {
            var text = "игра теннис";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFrequency.GetWordsFrequency(text)).Returns(new Dictionary<string, int>
                {[words[0]] = 1, [words[1]] = 1});

            _sut.GetWordTagsAndCloudRadius(text);

            A.CallTo(() => _wordMeasurer.GetWordSize(A<string>.Ignored, A<Font>.Ignored))
                .MustHaveHappened(text.Split(' ').Length, Times.Exactly);
        }

        [Test]
        public void GetWordTagsAndCloudRadius_BeCalledPutNextRectangleCertainNumber_WhenStringIsNotNull()
        {
            var text = "игра теннис";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFrequency.GetWordsFrequency(text)).Returns(new Dictionary<string, int>
                {[words[0]] = 1, [words[1]] = 1});

            _sut.GetWordTagsAndCloudRadius(text);

            A.CallTo(() => _cloudLayouter.PutNextRectangle(A<Size>.Ignored))
                .MustHaveHappened(text.Split(' ').Length, Times.Exactly);
        }

        [Test]
        public void GetWordTagsAndCloudRadius_CertainNumberTags_WhenStringIsNotNull()
        {
            var text = "игра теннис";
            var words = text.Split(' ');
            A.CallTo(() => _wordsFrequency.GetWordsFrequency(text)).Returns(new Dictionary<string, int>
                {[words[0]] = 1, [words[1]] = 1});

            var act = _sut.GetWordTagsAndCloudRadius(text);

            act.Item1.Should().HaveCount(words.Length);
        }
    }
}