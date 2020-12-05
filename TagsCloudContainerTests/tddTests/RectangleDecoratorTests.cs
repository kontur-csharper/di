using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class RectangleDecoratorTest
    {
        [Test]
        public void GetCorners_ReturnCornersOfRectangle()
        {
            var rect = new RectangleDecorator(
                new Rectangle(-1, 1, 2, 2));

            rect.GetCorners().Should().BeEquivalentTo(
                new[]
                {
                    new Point(-1, 1),
                    new Point(1, 1),
                    new Point(1, 3),
                    new Point(-1, 3),
                });
        }

        [Test]
        public void GetCorners_ReturnCornersOfRectangleClockwiseStartingFromLeftTop()
        {
            var rect = new RectangleDecorator(
                new Rectangle(0, 0, 2, 2));

            rect.GetCorners().Should().BeEquivalentTo(
                new[]
                {
                    new Point(0, 0),
                    new Point(2, 0),
                    new Point(2, 2),
                    new Point(0, 2),
                }, options => options.WithStrictOrdering());
        }
    }
}