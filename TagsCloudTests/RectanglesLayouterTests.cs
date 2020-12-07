using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloud.App;

namespace TagsCloudTests
{
    [TestFixture]
    public class RectanglesLayouterTests
    {
        [SetUp]
        public void SetUp()
        {
            layouter = new RectanglesLayouter(imageCenter);
        }

        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.Outcome == ResultState.Failure ||
                context.Result.Outcome == ResultState.Error)
            {
                var fileName = $"{context.WorkDirectory}\\{context.Test.Name}.png";
                var rectangles = layouter.Rectangles;
                TestContext.WriteLine($"Tag cloud visualization saved to file {fileName}");
                DrawRectangles(rectangles, imageCenter, new Size(800, 800), fileName);
            }
        }

        private Point imageCenter = new Point(400, 400);
        private RectanglesLayouter layouter;

        [Test]
        public void RectanglesLayouter_ShouldPlaceFirstRectangle_AtCenter()
        {
            var firstRectangle = layouter.PutNextRectangle(new Size(100, 70));
            firstRectangle
                .Should()
                .BeEquivalentTo(new Rectangle(
                    imageCenter.X - firstRectangle.Width / 2,
                    imageCenter.Y - firstRectangle.Height / 2,
                    firstRectangle.Width,
                    firstRectangle.Height));
        }

        [Test]
        public void RectanglesLayouter_ShouldPlaceSecondRectangle_NearFirst()
        {
            var firstRect = layouter.PutNextRectangle(new Size(100, 70));
            var secondRect = layouter.PutNextRectangle(new Size(70, 50));
            var firstRectCenterY = firstRect.Y + firstRect.Height / 2;
            var secondRectCenterY = secondRect.Y + secondRect.Height / 2;
            Math.Abs(firstRectCenterY - secondRectCenterY)
                .Should().Be((firstRect.Height + secondRect.Height) / 2,
                    "because secondhorizontal rectangle should be above or below first");
        }

        [Test]
        public void AnotherRectangle_ShouldBeClose_WithSomeRectanglesPlaced()
        {
            layouter.PutNextRectangle(new Size(50, 50));
            layouter.PutNextRectangle(new Size(50, 50));
            layouter.PutNextRectangle(new Size(50, 50));
            layouter.PutNextRectangle(new Size(50, 50));

            var rectangle = layouter.PutNextRectangle(new Size(50, 50));
            var distanceToCenter =
                RectanglesLayouter.CalculateDistance(imageCenter,
                    RectanglesLayouter.CalculateCenterPosition(rectangle));
            distanceToCenter.Should().Be(50, "because last closest position is near the central rectangle");
        }

        [Test]
        public void RectanglesLayouter_ShouldFormCircle_FromManyRectangles()
        {
            var rand = new Random();
            var letterSize = new Size(3, 4);
            var maxRadius = 0d;
            var rectanglesArea = 0d;
            for (var i = 0; i < 500; i++)
            {
                var wordSize = new Size(rand.Next(1, 100), rand.Next(1, 100));
                rectanglesArea += wordSize.Width * wordSize.Height;
                var newRect = layouter.PutNextRectangle(wordSize);
                maxRadius = Math.Max(maxRadius,
                    RectanglesLayouter.CalculateDistance(imageCenter,
                        RectanglesLayouter.CalculateCenterPosition(newRect)));
            }

            var circleArea = Math.PI * maxRadius * maxRadius;
            rectanglesArea.Should().BeGreaterOrEqualTo(circleArea * 0.8f);
        }

        private void DrawRectangles(List<Rectangle> rectangles, Point center, Size imageSize, string fileName)
        {
            var image = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black);
            graphics.DrawRectangles(new Pen(Color.Aqua, 2), rectangles.ToArray());
            graphics.FillEllipse(Brushes.Green, new Rectangle(center - new Size(2, 2), new Size(4, 4)));
            image.Save(fileName);
        }
    }
}