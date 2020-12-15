using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.PointGetters;
using TagCloud.CloudLayoters;

namespace TagCloudTests
{
    [TestFixture]
    class CircularCloudLayouterTest
    {
        private DensityCloudLayouter cloud;

        [SetUp]
        public void SetUp() => cloud = new DensityCloudLayouter(new CirclePointGetter(Point.Empty));


        [Test]
        public void Test_PutFirstRectangleInCenterCloud()
        {
            var random = new Random(0);
            for (var i = 0; i < 100; i++)
            {
                var center = new Point(random.Next(50), random.Next(50));
                var size = new Size(random.Next(1, 50), random.Next(1, 50));
                cloud = new DensityCloudLayouter(new CirclePointGetter(center));
                var r = cloud.PutNextRectangle(size);
                Assert.IsTrue(Math.Abs(center.X - r.X) <= (size.Width + 1) / 2 && Math.Abs(center.Y - r.Y) <= (size.Height + 1) / 2);
            }
        }

        [Test]
        public void Test_PutRectanglesWithoutIntersections()
        {
            var random = new Random(0);
            var rectangles = new HashSet<Rectangle>();
            for (var i = 0; i < 50; i++)
            {
                rectangles.Add(cloud.PutNextRectangle(new Size(random.Next(1, 10), random.Next(1, 10))));
            }
            var intersections = rectangles.SelectMany(r => rectangles.
                                                       Where(rec => rec != r).
                                                       Select(rec => Tuple.Create(r, rec)))
                                           .Select(t => t.Item1.IntersectsWith(t.Item2));
            Assert.IsFalse(intersections.Any(t => t));
        }

        [TestCase(100)]
        [TestCase(500)]
        [TestCase(1000)]
        public void Test_PutRectanglesInCircle(int square)
        {
            var rectangles = new HashSet<Rectangle>();
            for (var i = 0; i < square; i++)
                rectangles.Add(cloud.PutNextRectangle(new Size(1, 1)));
            var radius = Math.Sqrt(square / Math.PI);
            var distance = GetMaxAverageDistanceToPoint(rectangles, cloud.Center());
            Assert.IsTrue(distance <= radius);
        }

        [Test]
        public void Test_PutReactanglesTight()
        {
            var random = new Random(0);
            var rectangles = new HashSet<Rectangle>();
            for (var i = 0; i < 50; i++)
                rectangles.Add(cloud.PutNextRectangle(new Size(random.Next(1, 10), random.Next(1, 10))));
            Assert.IsFalse(rectangles.Any(r => !RectangleNextToRectangles(r, rectangles)));
        }

        private bool RectangleNextToRectangles(Rectangle rectangle, IEnumerable<Rectangle> rectangles) =>
            rectangles
            .Where(r => r != rectangle)
            .Any(r => RectanglesNextToEachOther(rectangle, r));

        private bool RectanglesNextToEachOther(Rectangle reactangle1, Rectangle rectangle2) =>
            GetAdjacentCellsWithoutAngles(reactangle1)
            .Any(r => r.IntersectsWith(rectangle2));

        private IEnumerable<Rectangle> GetAdjacentCellsWithoutAngles(Rectangle rectangle)
        {
            var result = new HashSet<Rectangle>();
            for (var i = rectangle.Left; i < rectangle.Right; i++)
            {
                result.Add(new Rectangle(i, rectangle.Top - 1, 1, 1));
                result.Add(new Rectangle(i, rectangle.Bottom, 1, 1));
            }
            for (var i = rectangle.Top; i < rectangle.Bottom; i++)
            {
                result.Add(new Rectangle(rectangle.Left - 1, i, 1, 1));
                result.Add(new Rectangle(rectangle.Right, i, 1, 1));
            }
            return result;
        }

        private double GetDistancePoint(PointF p1, PointF p2) =>
            Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

        private PointF GetCenterRectangle(Rectangle rectangle) =>
            new PointF((rectangle.X + rectangle.Width) / 2, (rectangle.Y + rectangle.Height) / 2);

        private double GetAverageDistanceToPoint(Rectangle rectangle, Point point) =>
            GetDistancePoint(GetCenterRectangle(rectangle), point);

        private double GetMaxAverageDistanceToPoint(IEnumerable<Rectangle> rectangles, Point point) =>
            rectangles
            .Select(r => GetAverageDistanceToPoint(r, point))
            .Max();
    }
}
