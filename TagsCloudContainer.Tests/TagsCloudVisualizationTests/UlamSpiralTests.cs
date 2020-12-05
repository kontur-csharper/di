using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudVisualizationTests
{
    public class UlamSpiralTests
    {
        private Point Center { get; set; }
        private UlamSpiral Spiral { get; set; }

        [SetUp]
        public void SetUp()
        {
            Center = new Point(500, 500);

            Spiral = new UlamSpiral(Center);
        }

        [Test]
        public void GetNextPoint_ReturnStartPoint_OnFirstRequest()
        {
            Spiral.GetNextPoint().Should().Be(Center);
        }

        [TestCase(-1, 0, 1, 1, TestName = "Center X coordinate is negative")]
        [TestCase(0, -1, 1, 1, TestName = "Center Y coordinate is negative")]
        public void ThrowException_When(
            int centerX, int centerY, double distanceBetweenLoops, double angleDelta)
        {
            var center = new Point(centerX, centerY);
            Func<ArchimedeanSpiral> sut = () => new ArchimedeanSpiral(center, distanceBetweenLoops, angleDelta);

            sut.Should().Throw<ArgumentException>();
        }
    }
}