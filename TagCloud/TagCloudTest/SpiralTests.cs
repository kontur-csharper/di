using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Curves;

namespace TagCloudTest
{
    [TestFixture]
    public class SpiralTests
    {
        [SetUp]
        public void SetUp()
        {
            curve = new ArchimedeanSpiral(new Point(0, 0));
        }

        private ArchimedeanSpiral curve;

        [Test]
        public void SpiralStartsAtCenter()
        {
            var startingPoint = new Point(1, 5);
            var shiftedSpiral = new ArchimedeanSpiral(startingPoint);

            shiftedSpiral.CurrentPoint.Should().Be(startingPoint);
        }

        [Test]
        public void DistanceBetween2PointsAfterFullSpin_ShouldIncrease()
        {
            curve.Next();
            var point1 = curve.CurrentPoint;

            for (var i = 0; i < 360 / curve.AngleStep; i++)
                curve.Next();
            var point2 = curve.CurrentPoint;

            for (var i = 0; i < 360 / curve.AngleStep; i++)
                curve.Next();
            var point3 = curve.CurrentPoint;

            int DistSquared(Point p1, Point p2)
            {
                return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
            }

            DistSquared(point1, point2).Should().BeGreaterThan(DistSquared(point2, point3));
        }
    }
}