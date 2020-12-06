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
    public class WordsFilterShould
    {
        private WordsFilter _sut;
        private INormalizedWordAndSpeechPartParser normalizedWordAndSpeechPartParser;
        private ITextProcessingSettings _textProcessingSettings;

        [SetUp]
        public void SetUp()
        {
            normalizedWordAndSpeechPartParser = A.Fake<INormalizedWordAndSpeechPartParser>();
            _textProcessingSettings = A.Fake<ITextProcessingSettings>();
            _sut = new WordsFilter(_textProcessingSettings);
        }

        [Test]
        public void GetInterestingWords_ThrowException_WhenStringIsNull()
        {
            var act = new Action(() => _sut.GetInterestingWords(null));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetInterestingWords_BeNotCalledParseToPartSpeechAndWords_WhenStringIsNull()
        {
            try
            {
                var _ = new Action(() => _sut.GetInterestingWords(null));
            }
            catch
            {
                // ignored
            }

            A.CallTo(() => normalizedWordAndSpeechPartParser.ParseToNormalizedWordAndPartSpeech(A<string>.Ignored))
                .MustNotHaveHappened();
        }

        [Test]
        public void GetInterestingWords_Words_WhenStringContainsInterestingWords()
        {
            var words = new[] {"собака", "кот", "в", "она", "подвал"};
            A.CallTo(() => _textProcessingSettings.BoringWords).Returns(new HashSet<string> {words.Last()});

            var act = _sut.GetInterestingWords(words);

            act.Should().BeEquivalentTo(words[0], words[1], words[2], words[3]);
        }
    }
}