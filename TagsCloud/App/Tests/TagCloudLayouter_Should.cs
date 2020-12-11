using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloud.App.Tests
{
    public class TagCloudLayouter_Should
    {
        private Point center;

        private TagCloudLayouter layouter;
        private List<Rectangle> placedRectangles;

        [SetUp]
        public void SetUp()
        {
            center = new Point(10, 10);
            layouter = new TagCloudLayouter(new SpiralAlgorithm(center));
            placedRectangles = new List<Rectangle>();
        }

        [Test]
        public void PutRectangleWithCorrectSize()
        {
            var size = new Size(5, 5);

            placedRectangles.Add(layouter.PutNextRectangle(size));

            placedRectangles[0].Size.Should().Be(size);
        }

        [TestCaseSource(nameof(GetDataForTestRectangleCenter))]
        public void PutFirstRectangleInCenter(Size rectangleSize)
        {
            placedRectangles.Add(layouter.PutNextRectangle(rectangleSize));

            GetRectangleCenter(placedRectangles[0]).Should().Be(center);
        }

        [TestCaseSource(nameof(GetDataForTestIntersectionRectangles))]
        public void PutNotIntersectedRectangles(Size[] rectanglesSizes)
        {
            placedRectangles =
                rectanglesSizes.Select(rectangleSize => layouter.PutNextRectangle(rectangleSize)).ToList();

            for (var i = 0; i < rectanglesSizes.Length; i++)
            for (var j = i + 1; j < rectanglesSizes.Length; j++)
                placedRectangles[i].IntersectsWith(placedRectangles[j]).Should().BeFalse();
        }

        [TestCaseSource(nameof(GetDataForTestPuttingRectangleInCircle))]
        public void PutRectanglesInCircle(Size[] rectanglesSizes, double maxRadius)
        {
            placedRectangles =
                rectanglesSizes.Select(rectangleSize => layouter.PutNextRectangle(rectangleSize)).ToList();

            foreach (var placedRectangle in placedRectangles)
                GetDistanceToCenter(placedRectangle).Should().BeLessOrEqualTo(maxRadius);
        }

        [Test]
        public void PutEmptyRectangle_WhenSizeIsEmpty() =>
            layouter.PutNextRectangle(new Size(0, 0)).Should().Be(Rectangle.Empty);

        private Point GetRectangleCenter(Rectangle rectangle)
        {
            var x = rectangle.Left + rectangle.Width / 2;
            var y = rectangle.Top + rectangle.Height / 2;
            return new Point(x, y);
        }

        private double GetDistanceToCenter(Rectangle rectangle)
        {
            var rectangleCenter = GetRectangleCenter(rectangle);
            return Math.Sqrt((rectangleCenter.X - center.X) * (rectangleCenter.X - center.X)
                             + (rectangleCenter.Y - center.Y) * (rectangleCenter.Y - center.Y));
        }

        private static IEnumerable GetDataForTestPuttingRectangleInCircle()
        {
            yield return new TestCaseData(GetRectanglesSizes(25, 1, 1), Math.Sqrt(8))
                .SetName("when rectangles square = 1");
            yield return new TestCaseData(GetRectanglesSizes(8, 2, 2), Math.Sqrt(8))
                .SetName("when rectangles square = 2");
        }

        private static IEnumerable GetDataForTestRectangleCenter()
        {
            yield return new TestCaseData(new Size(1, 1))
                .SetName("when rectangle square = 1");
            yield return new TestCaseData(new Size(2, 1))
                .SetName("when rectangle width is even");
            yield return new TestCaseData(new Size(1, 2))
                .SetName("when rectangle height is even");
            yield return new TestCaseData(new Size(3, 1))
                .SetName("when rectangle width is odd and > 1");
            yield return new TestCaseData(new Size(1, 3))
                .SetName("when rectangle height is odd and > 1");
            yield return new TestCaseData(new Size(5, 8))
                .SetName("when rectangle height and width > 1");
        }

        private static IEnumerable GetDataForTestIntersectionRectangles()
        {
            var rectanglesSizes = new[]
            {
                new Size(1, 1),
                new Size(1, 1)
            };
            yield return new TestCaseData(rectanglesSizes)
                .SetName("when rectangle square = 1");

            rectanglesSizes = new[]
            {
                new Size(5, 5),
                new Size(2, 8),
                new Size(7, 3),
                new Size(1, 1),
                new Size(3, 4),
                new Size(2, 2)
            };
            yield return new TestCaseData(rectanglesSizes)
                .SetName("when rectangle square > 1");
        }

        private static Size[] GetRectanglesSizes(int count, int width, int height)
        {
            var rectanglesSizes = new Size[count];
            for (var i = 0; i < rectanglesSizes.Length; i++) rectanglesSizes[i] = new Size(width, height);
            return rectanglesSizes;
        }
    }
}