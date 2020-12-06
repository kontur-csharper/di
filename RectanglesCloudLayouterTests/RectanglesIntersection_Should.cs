using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using RectanglesCloudLayouter.SpecialMethods;

namespace RectanglesCloudLayouterTests
{
    [TestFixture]
    public class RectanglesIntersectionShould
    {
        private List<Rectangle> _rectangles;

        [SetUp]
        public void SetUp()
        {
            _rectangles = new List<Rectangle>
            {
                new Rectangle(new Point(0, 0), new Size(5, 5)),
                new Rectangle(new Point(7, -5), new Size(1, 3)),
                new Rectangle(new Point(-7, -5), new Size(5, 10))
            };
        }

        [TestCase(-5, -4, 2, 3)]
        [TestCase(1, 2, 5, 7)]
        public void IsAnyIntersectWithRectangles_ExistIntersections_WhenIntersectionAreaIsPositive(int coordinateX,
            int coordinateY, int width,
            int height)
        {
            var rectangle = new Rectangle(new Point(coordinateX, coordinateY), new Size(width, height));

            var isIntersect = RectanglesIntersection.IsAnyIntersectWithRectangles(rectangle, _rectangles);

            isIntersect.Should().BeTrue();
        }

        [TestCase(5, 5, 3, 7)]
        [TestCase(6, 6, 3, 7)]
        public void IsAnyIntersectWithRectangles_NotIntersections_WhenIntersectionAreaIsZero(int coordinateX,
            int coordinateY, int width,
            int height)
        {
            var rectangle = new Rectangle(new Point(coordinateX, coordinateY), new Size(width, height));

            var isIntersect = RectanglesIntersection.IsAnyIntersectWithRectangles(rectangle, _rectangles);

            isIntersect.Should().BeFalse();
        }
    }
}