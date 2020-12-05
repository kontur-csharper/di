using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudVisualizationTests
{
    public class ArchimedeanSpiralTests
    {
        private Point Center { get; set; }
        private ArchimedeanSpiral Spiral { get; set; }

        [SetUp]
        public void SetUp()
        {
            Center = new Point(500, 500);
            const double distanceBetweenLoops = 1;
            const double angleDelta = 1;

            Spiral = new ArchimedeanSpiral(Center, distanceBetweenLoops, angleDelta);
        }

        [Test]
        public void GetNextPoint_ReturnStartPoint_OnFirstRequest()
        {
            Spiral.GetNextPoint().Should().Be(Center);
        }

        [TestCase(-1, 0, 1, -1, TestName = "angleDelta is negative")]
        [TestCase(-1, 0, 1, 0, TestName = "angleDelta is zero")]
        [TestCase(-1, 0, 1, 1, TestName = "Center X coordinate is negative")]
        [TestCase(0, -1, 1, 1, TestName = "Center Y coordinate is negative")]
        [TestCase(0, 0, 0, 1, TestName = "distanceBetweenLoops is zero")]
        [TestCase(0, 0, -1, 1, TestName = "distanceBetweenLoops is negative")]
        public void ThrowException_When(
            int centerX, int centerY, double distanceBetweenLoops, double angleDelta)
        {
            var center = new Point(centerX, centerY);
            Func<ArchimedeanSpiral> sut = () => new ArchimedeanSpiral(center, distanceBetweenLoops, angleDelta);

            sut.Should().Throw<ArgumentException>();
        }
    }
}