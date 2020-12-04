using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudTests.CloudLayouterTests
{
    internal class PointExtensionsTests
    {
        [TestCase(0, 0, 0, 0, 0, TestName = "GetDistanceTo_ShouldBeZero_WhenPointsAreEqual")]
        [TestCase(0, 0, 0, 3, 3, TestName = "GetDistanceTo_ShouldBeCorrect_WhenXsAreEqual")]
        [TestCase(0, 0, 3, 0, 3, TestName = "GetDistanceTo_ShouldBeCorrect_WhenYsAreEqual")]
        [TestCase(0, 0, 3, 4, 5, TestName = "GetDistanceTo_ShouldBeCorrect_WhenXsYsAreDifferent")]
        [TestCase(0, 0, -3, -4, 5, TestName = "GetDistanceTo_ShouldBeCorrect_WhenCoordinatesAreNegative")]
        public void TestForGetDistanceTo(int x1, int y1, int x2, int y2, double expectedDistance)
        {
            var actualDistance = new Point(x1, y1).GetDistanceTo(new Point(x2, y2));
            actualDistance.Should().BeInRange(expectedDistance - 1e-9, expectedDistance + 1e-9);
        }
    }
}