using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloud.App.Tests
{
    public class SpiralAlgorithm_Should
    {
        private Point center;
        private SpiralAlgorithm spiral;

        [SetUp]
        public void SetUp()
        {
            center = new Point(10, 10);
            spiral = new SpiralAlgorithm(center);
        }

        [Test]
        public void ReturnsCenterAtFirst() => spiral.First().Should().Be(center);

        [Test]
        public void NotReturnsCenterAtFirst_IfCenterWasAddedToUsedPoints()
        {
            spiral.AddUsedPoints(new[] {center});
            spiral.First().Should().NotBe(center);
        }

        [Test]
        public void ReturnsSpiral()
        {
            var expected = new[]
            {
                new Point(10, 10), new Point(9, 10), new Point(9, 9), new Point(10, 9),
                new Point(11, 9), new Point(11, 10), new Point(11, 11), new Point(10, 11),
                new Point(9, 11), new Point(8, 11)
            };
            spiral.Take(10).Should().BeEquivalentTo(expected);
        }
    }
}