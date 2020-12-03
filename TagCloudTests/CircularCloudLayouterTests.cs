using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Core.LayoutAlgorithms;
using TagCloud.Extensions;

namespace TagCloudTests
{
    class CircularCloudLayouterTests
    {
        private Point center;
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            center = new Point(300, 300);
            layouter = new CircularCloudLayouter(new ArchimedeanSpiral(center));
        }

        [TestCase(0, 0, TestName = "CenterAtOrigin")]
        [TestCase(2, 2, TestName = "CenterInFirstQuarter")]
        [TestCase(-2, -2, TestName = "CenterInThirdQuarter")]
        [TestCase(2, -2, TestName = "CenterCoordinatesHaveDifferentSigns")]
        public void CircularCloudLayouterConstructor_DoesNotThrow(int x, int y)
        {
            var start = new Point(x, y);
            Assert.DoesNotThrow(
                () => new CircularCloudLayouter(new ArchimedeanSpiral(start)));
        }

        [TestCase(0, TestName = "ZeroWhenNoPlaced")]
        [TestCase(5, TestName = "FiveWhenFivePlaced")]
        [TestCase(50, TestName = "FiftyWhenFiftyPlaced")]
        public void PutNextRectangle_RectanglesAmountAsSameAsPlaced(
            int rectsCount)
        {
            PutRectangles(rectsCount);
            layouter.Rectangles.Count().Should().Be(rectsCount);
        }

        [Test]
        public void PutNextRectangle_ShouldPlaceFirstRectangleToCenter()
        {
            var rect = layouter.PutNextRectangle(new Size(10, 10));
            rect.Location.Should().Be(center);
        }

        [TestCase(2, TestName = "TwoRectangles")]
        [TestCase(60, TestName = "SixtyRectangles")]
        [TestCase(100, TestName = "OneHundredRectangles")]
        public void PutNextRectangle_ShouldPlaceRectangleWithoutIntersection(
            int rectsCount)
        {
            PutRectangles(rectsCount, 11, 22);
            HaveAnyIntersections(layouter.Rectangles).Should().BeFalse();
        }

        [TestCase(20, 5, 5, TestName = "FourRectangles")]
        [TestCase(50, 7, 7, TestName = "FiftyRectangles")]
        [TestCase(100, 10, 10, TestName = "OneHundredRectangles")]
        public void PutNextRectangle_ShouldKeepCloudShapeCloseToCircle(
            int rectsCount, int width, int height)
        {
            var layoutArea = rectsCount * width * height;
            PutRectangles(rectsCount, width, height);

            var expectedRadius = Math.Sqrt(layoutArea / Math.PI);
            var maxDelta = center.GetMaxDistanceToLayoutBorder(layouter.Rectangles);

            ((double) maxDelta.X).Should().BeLessThan(expectedRadius * 1.25);
            ((double) maxDelta.Y).Should().BeLessThan(expectedRadius * 1.25);
        }

        [TestCase(4, TestName = "FourRectangles")]
        [TestCase(20, TestName = "TwentyRectangles")]
        [TestCase(50, TestName = "FiftyRectangles")]
        public void PutNextRectangle_ShouldPutRectanglesTightly(int rectsCount)
        {
            var size = new Size(5, 5);
            for (var i = 0; i < rectsCount - 1; i++)
                layouter.PutNextRectangle(size);

            var rect = layouter.PutNextRectangle(size);
            Math.Abs(rect.X - center.X).Should()
                .BeLessOrEqualTo(rectsCount * size.Width / (rectsCount / 4));
            Math.Abs(rect.Y - center.Y).Should()
                .BeLessOrEqualTo(rectsCount * size.Height / (rectsCount / 4));
        }

        [Test]
        public void GetLayoutSize_ShouldThrowArgumentException_NoRectangles()
        {
            Assert.Throws<ArgumentException>(() => layouter.GetLayoutSize());
        }

        private static bool HaveAnyIntersections(IEnumerable<Rectangle> rects)
        {
            var rectangles = rects.ToList();
            var intersectedRectsCount = rectangles
                .Select((item, index) => rectangles
                    .Skip(index).Count(item.IntersectsWith)).Sum();

            return intersectedRectsCount == 0;
        }

        private void PutRectangles(int amountToPlace,
            int width = 10, int height = 10)
        {
            for (var i = 0; i < amountToPlace; i++)
                layouter.PutNextRectangle(new Size(width, height));
        }
    }
}