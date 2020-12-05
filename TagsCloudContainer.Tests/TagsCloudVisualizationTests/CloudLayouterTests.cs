using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudVisualizationTests
{
    public class CloudLayouterTests
    {
        private Point Center { get; set; }
        private RectangleVisualizer RectangleVisualizer { get; set; }
        private CloudLayouter Sut { get; set; }

        [SetUp]
        public void SetUp()
        {
            const double distanceBetweenLoops = 0.2;
            const double angleDelta = 1;

            RectangleVisualizer = new RectangleVisualizer();
            Center = new Point(300, 300);
            Sut = new CloudLayouter(new ArchimedeanSpiral(Center, distanceBetweenLoops, angleDelta));
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var path = new PathGenerator(new DateTimeProvider()).GetNewFilePath();
                new BitmapSaver().SaveBitmapToDirectory(RectangleVisualizer.GetBitmap(Sut.Rectangles), path);

                Console.WriteLine($"Tag cloud visualization saved to file {path}");
            }
        }

        [TestCase(0, 9, TestName = "Width is zero")]
        [TestCase(-1, 8, TestName = "Width is negative")]
        [TestCase(10, 0, TestName = "Height is zero")]
        [TestCase(3, -2, TestName = "Height is negative")]
        public void PutNextRectangle_ThrowException_When(int width, int height)
        {
            var size = new Size(width, height);
            Action putRectangle = () => Sut.PutNextRectangle(size);

            putRectangle.Should().Throw<ArgumentException>();
            Assert.Throws<ArgumentException>(() => Sut.PutNextRectangle(size));
        }

        [Test]
        public void PutNextRectangle_ReturnExpectedSize_WhenSizeIsPositive()
        {
            var expectedSize = new Size(13, 11);
            var rectangle = Sut.PutNextRectangle(expectedSize);

            rectangle.Size.Should().Be(expectedSize);
        }

        [TestCase(2, TestName = "called two times")]
        [TestCase(1000, TestName = "called many times")]
        public void PutNextRectangle_ReturnNotCollidedRectangles_When(int rectanglesCount)
        {
            var rectangles = new List<Rectangle>();

            for (var i = 0; i < rectanglesCount; i++)
            {
                var newRectangle = Sut.PutNextRectangle(new Size(50, 50));

                rectangles.Any(rectangle => rectangle.IntersectsWith(newRectangle)).Should().BeFalse();
                rectangles.Add(newRectangle);
            }
        }
    }
}