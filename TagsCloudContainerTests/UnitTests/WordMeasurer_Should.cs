using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainerTests.UnitTests
{
    public class WordMeasurerShould
    {
        private WordMeasurer _sut;
        private static string _word = "ученик";
        private static Font _font = new Font("arial", 7);

        [SetUp]
        public void SetUp()
        {
            _sut = new WordMeasurer();
        }

        [Test]
        public void GetWordSize_ThrowException_WhenWordIsNull()
        {
            var act = new Action(() => _sut.GetWordSize(null, _font));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetWordSize_ThrowException_WhenFontIsNull()
        {
            var act = new Action(() => _sut.GetWordSize(_word, null));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetWordSize_ThrowException_WhenWordAndFontAreNull()
        {
            var act = new Action(() => _sut.GetWordSize(null, null));

            act.Should().Throw<Exception>();
        }

        [Test]
        public void GetWordSize_NotException_WhenWordAndFontNotNull()
        {
            var act = new Action(() => _sut.GetWordSize(_word, _font));

            act.Should().NotThrow<Exception>();
        }

        [Test]
        public void GetWordSize_Size_WhenWordAndFontNotNull()
        {
            var sizeF = Graphics.FromHwnd(IntPtr.Zero).MeasureString(_word, _font);
            var expected = new Size((int) Math.Ceiling(sizeF.Width), (int) Math.Ceiling(sizeF.Height));

            var act = _sut.GetWordSize(_word, _font);

            act.Should().Be(expected);
        }
    }
}