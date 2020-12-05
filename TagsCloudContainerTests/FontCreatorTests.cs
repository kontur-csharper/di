using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class FontCreatorTests
    {
        private FontCreator creator;

        [SetUp]
        public void SetUp()
        {
            creator = new FontCreator("Arial");
        }

        [Test]
        public void GetFontSize_CorrectCalculateSize()
        {
            var max = creator.MaxFontSize;
            var min = creator.MinFontSize;
            var expectedSize = ((float)2 / 10) * (max - min) + min;
            
            creator.GetFontSize(2, 10).Should().Be(expectedSize);
        }

        [Test]
        public void GetFontName_ReturnFontNameFromCtor()
        {
            creator.GetFontName(2, 10).Should().Be("Arial");
        }
    }
}