using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using RectanglesCloudLayouter.SpecialMethods;

namespace RectanglesCloudLayouterTests
{
    [TestFixture]
    public class PointsDistanceShould
    {
        [TestCase(1, 5, 1, 5)]
        [TestCase(-7, 7, -7, 7)]
        public void GetDistanceBetweenPoint_ZeroDistance_WhenPointsAreEquivalent(int firstX, int firstY,
            int secondX, int secondY)
        {
            var firstPoint = new Point(firstX, firstY);
            var secondPoint = new Point(secondX, secondY);

            var distanceBetweenPoints = PointsDistance.GetCeilingDistanceBetweenPoints(firstPoint, secondPoint);

            distanceBetweenPoints.Should().Be(0);
        }

        [TestCase(5, -1, 1, 2, 5)]
        [TestCase(5, -1, 5, 2, 3)]
        [TestCase(3, -1, 5, -1, 2)]
        [TestCase(5, -1, 2, -3, 4)]
        public void GetDistanceBetweenPoint_PositiveDistance_WhenPointsNotEquivalent(int firstX, int firstY,
            int secondX, int secondY, int expectedDistance)
        {
            var firstPoint = new Point(firstX, firstY);
            var secondPoint = new Point(secondX, secondY);

            var distanceBetweenPoints = PointsDistance.GetCeilingDistanceBetweenPoints(firstPoint, secondPoint);

            distanceBetweenPoints.Should().Be(expectedDistance);
        }
    }
}