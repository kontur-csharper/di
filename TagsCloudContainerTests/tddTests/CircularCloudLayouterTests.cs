using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter layouter;
        private List<Rectangle> rectangles;

        [SetUp]
        public void SetUp()
        {
            rectangles = new List<Rectangle>();
            layouter = new CircularCloudLayouter(new Point(25, 25), new Spiral());
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure)
                return;

            var fileName = $"{TestContext.CurrentContext.Test.FullName}.png";
            var path = Path.Combine(Path.GetFullPath(@"..\..\..\"), fileName);
            
            var image = Drawer.DrawRectangles(rectangles);
            image.Save(path);

            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }

        [Test]
        public void PutNextRectangle_LocationOfFirstRectangleIsCenter()
        {
            rectangles.Add(
                layouter.PutNextRectangle(new Size(2, 2)));

            rectangles.Should().ContainSingle(rectangle =>
                rectangle.Location == layouter.Center - rectangle.Size/2);
        }

        [Test]
        public void PutNextRectangle_AllRectanglesAreNotIntersect()
        {
            PutNextRectangleTest();
        }

        [TestCase(-5, -5, TestName = "Center at (-5, -5)")]
        [TestCase(400, 400, TestName = "Center at (400, 400)")]
        [TestCase(0, 0, TestName = "Center at (0, 0)")]
        public void PutNextRectangle_CorrectWorkWithDifferentCenters(int xCenter, int yCenter)
        {
            layouter = new CircularCloudLayouter(new Point(xCenter, yCenter), new Spiral());
            PutNextRectangleTest();
        }

        private void PutNextRectangleTest(int count = 100)
        {
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var size = new Size(random.Next(5, 80), random.Next(5, 80));
                rectangles.Add(layouter.PutNextRectangle(size));
            }

            AreRectangleIntersect(rectangles).Should().Be(false);
        }

        private static bool AreRectangleIntersect(List<Rectangle> rects)
        {
            return rects.Any(rectangle =>
                rects.Where(otherRect => otherRect != rectangle)
                    .Any(rectangle.IntersectsWith));
        }
    }
}