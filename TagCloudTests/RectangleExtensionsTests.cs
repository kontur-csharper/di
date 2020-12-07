using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Extensions;

namespace TagCloudTests
{
    public class RectangleExtensionsTests
    {

        [Test]
        public void IntersectsWith_ShouldTrue_WhenEnumerableContainsIntersectingRectangle()
        {
            var rectangles = new Rectangle[]
            {
                new Rectangle(-1, -1, 2, 2)
            };

            var rectangle = new Rectangle(0, 0, 2, 2);

            rectangle.IntersectsWith(rectangles).Should().BeTrue();
        }

        [Test]
        public void IntersectsWith_ShouldFalse_WhenEnumerableDoNotContainsIntersectingRectangle()
        {
            var rectangles = new[]
            {
                new Rectangle(-1, -1, 2, 2)
            };

            var rectangle = new Rectangle(1, 1, 2, 2);

            rectangle.IntersectsWith(rectangles).Should().BeFalse();
        }

        [Test]
        public void GetMiddlePoint_ShouldReturnCorrectPoint()
        {
            var rectangle = new Rectangle(0, 0, 4, 4);

            var expectedPoint = new Point(2, 2);

            rectangle.GetMiddlePoint().Should().BeEquivalentTo(expectedPoint);
        }

        [Test]
        public void CreateRectangleFromMiddlePointAndSize_ShouldReturnCorrectRectangle()
        {
            var expectedRectangle = new Rectangle(new Point(0, 0), new Size(4, 4));

            var result = RectangleExtensions.CreateRectangleFromMiddlePointAndSize(new Point(2, 2), new Size(4, 4));

            result.Should().BeEquivalentTo(expectedRectangle);
        }

        [Test]
        public void MoveOneStepTowardsPoint_ShouldReturnCorrectRectangle()
        {
            var expectedRectangle = new Rectangle(new Point(2, 2), new Size(2, 2));
            var rectangle = new Rectangle(new Point(3, 3), new Size(2, 2));
            var moveToPoint = new Point(0, 0);

            var result = rectangle.MoveOneStepTowardsPoint(moveToPoint, 1);

            result.Should().BeEquivalentTo(expectedRectangle);
        }
    }
}
