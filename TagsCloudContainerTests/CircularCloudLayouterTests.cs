using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private Point center;
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            var options = new OptionsStub(500, 500);
            center = new Point(options.Width / 2, options.Height / 2);
            layouter = new CircularCloudLayouter();
            layouter.SetCenter(new Point(center.X, center.Y));
        }

        [Test]
        public void FirstRectangle_ShouldBeCentral()
        {
            layouter.PutNextRectangle(new Size(10, 10)).Location
                .Should().Be(new Point(center.X - 5, center.Y - 5));
        }

        [Test]
        public void Rectangles_ShouldntIntersect()
        {
            var random = new Random();
            var rectangles = Enumerable
                .Range(0, 200)
                .Select(_ => new Size(random.Next(100, 200), random.Next(50, 100)))
                .Select(size => layouter.PutNextRectangle(size))
                .ToList();

            for (var i = 0; i < rectangles.Count; i++)
            {
                for (var j = i + 1; j < rectangles.Count; j++)
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
            }
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(0, 0)]
        public void ShouldThrowOnIncorrectRectangle(int width, int height)
        {
            Action action = () => { layouter.PutNextRectangle(new Size(width, height)); };
            action.Should().Throw<ArgumentException>().WithMessage("Rectangle size should be positive.");
        }

        [Test]
        public void PutNextRectangle_ShouldReturnCorrectRectangle()
        {
            var rng = new Random();
            for (var i = 0; i < 100; i++)
            {
                var size = new Size(rng.Next(10, 100), rng.Next(10, 100));
                layouter.PutNextRectangle(size).Size.Should().Be(size);
            }
        }

        [Test]
        public void Rectangles_ShouldBeInsideCircle()
        {
            var random = new Random();
            var rectangles = Enumerable
                .Range(0, 200)
                .Select(_ => new Size(random.Next(10, 100), random.Next(10, 100)))
                .Select(size => layouter.PutNextRectangle(size))
                .ToList();

            var radius = GetCircleRadius(rectangles);

            foreach (var rectangle in rectangles)
            {
                var distanceToCenter = GetMaximumDistance(rectangle, center);
                distanceToCenter.Should().BeLessThan(radius);
            }
        }

        private static int GetCircleRadius(IEnumerable<Rectangle> rectangles)
        {
            const double radiusMultiplier = 1.25;
            var square = rectangles
                .Select(rectangle => rectangle.Width * rectangle.Height)
                .Sum();
            return (int) (Math.Sqrt(square / Math.PI) * radiusMultiplier);
        }

        private static double GetMaximumDistance(Rectangle rectangle, Point center)
        {
            var maxX = Math.Max(Math.Abs(center.X - rectangle.X), Math.Abs(center.X - rectangle.X - rectangle.Width));
            var maxY = Math.Max(Math.Abs(center.Y - rectangle.Y), Math.Abs(center.Y - rectangle.Y - rectangle.Height));
            return Math.Sqrt(maxX * maxX + maxY * maxY);
        }
    }
}