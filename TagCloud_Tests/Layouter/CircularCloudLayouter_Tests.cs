using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using TagCloud.Layouters;
using TagCloud.Settings;
using TagCloud_Tests.Layouter;

namespace TagsCloud_Tests.Layouter
{
    [TestFixture]
    public class CircularCloudLayouter_Tests
    {
        private IRectangleLayouter layouter;
        private Size minSize;
        private Size maxSize;
        private const double MinOccupiedAreaRatio = 0.65;
        private static readonly Point layoutCenter = new Point(1000, 1000);

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter(new CircularLayouterSettings(layoutCenter, 4, 0.005));
            minSize = new Size(5, 5);
            maxSize = new Size(20, 20);
        }
        
        [Test]
        public void PutNextRectangle_ShouldReturnRectangleWithSameSize_WhenSomeSizesAdded()
        {
            foreach (var size in SizesGenerator.GenerateSizes(5, minSize, maxSize, seed:128))
                layouter.PutNextRectangle(size).Size.Should().BeEquivalentTo(size);
        }
        
        [Test]
        public void Layout_ShouldContainsAllRectangles_WhenSomeSizesAdded()
        {
            var sizes = SizesGenerator.GenerateSizes(5, minSize, maxSize, seed:128);
            var rectangles = FillLayoutWithSomeRectangles(layouter, sizes);

            rectangles.Select(rect => rect.Size).Should().BeEquivalentTo(sizes);
        }
        
        [Test]
        public void LayoutRectangles_ShouldNotIntersectEachOther_WhenSomeRectanglesAdded()
        {
            var sizes = SizesGenerator.GenerateSizes(25, minSize, maxSize, seed:128);
            FillLayoutWithSomeRectangles(layouter, sizes);

            var rectangles = new List<Rectangle>();
            foreach (var rectangle in rectangles)
            {
                IsRectangleIntersectOther(rectangles, rectangle).Should().BeFalse("rectangles must not intersect");
            }
        }
        
        [Test]
        public void LayoutOccupiedArea_ShouldBeGreaterOrEqualThanMinimal_WhenManySizesAdded()
        {
            var sizes = SizesGenerator.GenerateSizes(600, minSize, maxSize, seed:128);
            var rectangles = FillLayoutWithSomeRectangles(layouter, sizes);
            
            var occupiedArea = rectangles.Sum(rectangle => rectangle.Width * rectangle.Height);

            var maxLayoutRadius = 0.0;

            foreach (var rectangle in rectangles)
            {
                var maxDistanceToRectangle = GetMaxDistanceToRectangle(layoutCenter, rectangle);
                if (maxLayoutRadius < maxDistanceToRectangle)
                    maxLayoutRadius = maxDistanceToRectangle;
            }

            var totalArea = GetCircleAreaFromRadius(maxLayoutRadius);
            var actualOccupiedAreaRatio = occupiedArea / totalArea;
            actualOccupiedAreaRatio.Should().BeGreaterOrEqualTo(MinOccupiedAreaRatio);
        }

        private static double GetMaxDistanceToRectangle(Point center, Rectangle rectangle)
        {
            var cornerX = GetNumberWithBiggerDistanceFromGiven(
                center.X, 
                rectangle.X, 
                rectangle.X + rectangle.Width);
            var cornerY = GetNumberWithBiggerDistanceFromGiven(
                center.Y, 
                rectangle.Y, 
                rectangle.Y + rectangle.Height);
            return GetDistance(center, new Point(cornerX, cornerY));
        }
        
        private static int GetNumberWithBiggerDistanceFromGiven(int givenNumber, 
            int firstNumber, int secondNumber)
        {
            return Math.Abs(givenNumber - firstNumber) >= Math.Abs(givenNumber - secondNumber)
                ? firstNumber
                : secondNumber;
        } 
        
        private static double GetCircleAreaFromRadius(double radius) 
            => Math.PI * Math.Pow(radius, 2);
        
        private static double GetDistance(Point first, Point second)
            => Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        
        private static List<Rectangle> FillLayoutWithSomeRectangles(IRectangleLayouter layouter,
            IEnumerable<Size> rectangleSizes)
        {
            var result = new List<Rectangle>();
            foreach (var size in rectangleSizes)
                result.Add(layouter.PutNextRectangle(size));
            return result;
        }

        private static bool IsRectangleIntersectOther(List<Rectangle> rectangles, Rectangle rectangle)
        {
            foreach (var otherRectangle in rectangles)
            {
                if (rectangle != otherRectangle 
                    && rectangle.IntersectsWith(otherRectangle))
                    return true;
            }
            return false;
        }
    }
}
