using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Common;

namespace TagsCloudTests.CloudLayouterTests
{
    public class SpiralCloudLayouterTests
    {
        private readonly int rectanglesCount = 70;
        private ImageSettings imageSettings;
        private SpiralCloudLayouter spiralCloudLayouter;

        [SetUp]
        public void SetUp()
        {
            imageSettings = new ImageSettings();
            spiralCloudLayouter = new SpiralCloudLayouter(imageSettings);
        }


        private Size MakeRandomSize()
        {
            var rand = new Random(Environment.TickCount);
            return new Size(rand.Next(10, 50), rand.Next(10, 30));
        }

        [Test]
        public void PutNextRectangle_PlaceFirstRectangleOnCenter()
        {
            spiralCloudLayouter.Rectangles[0].X.Should().Be(imageSettings.Width / 2);
            spiralCloudLayouter.Rectangles[0].Y.Should().Be(imageSettings.Height / 2);
        }

        [Test]
        public void PutNextRectangle_PlaceFirstRectangleOnCenter_WhenOnlyOneRectangle()
        {
            var rectangle = new SpiralCloudLayouter(imageSettings)
                .PutNextRectangle(MakeRandomSize());
            rectangle.X.Should().Be(imageSettings.Width / 2);
            rectangle.Y.Should().Be(imageSettings.Height / 2);
        }

        [Test]
        public void PutNextRectangle_ThrowsException_RectangleSizeLessOrEqualZero()
        {
            Assert.Throws<ArgumentException>(() => spiralCloudLayouter.PutNextRectangle(new Size(0, 0)));
        }

        [Test]
        public void PutNextRectangle_NotCross_PreviousRectangle()
        {
            for (var i = 1; i < rectanglesCount; i++)
                spiralCloudLayouter.Rectangles[i - 1].IntersectsWith(spiralCloudLayouter.Rectangles[i]).Should()
                    .BeFalse();
        }

        [Test]
        public void PutNextRectangle_NotCross_AllPlacedRectangles()
        {
            for (var i = 0; i < rectanglesCount; i++)
            for (var j = i + 1; j < rectanglesCount; j++)
                spiralCloudLayouter.Rectangles[i].IntersectsWith(spiralCloudLayouter.Rectangles[j]).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_PlaceRectanglesTightly()
        {
            var squareOfRectangles = 0.0;
            var radius = 0.0;
            for (var i = 0; i < rectanglesCount; i++)
            {
                radius = Math.Max(
                    GetRadiusOfFramingCircle(spiralCloudLayouter.Rectangles[0].Location,
                        spiralCloudLayouter.Rectangles[i]), radius);
                squareOfRectangles +=
                    spiralCloudLayouter.Rectangles[i].Height * spiralCloudLayouter.Rectangles[i].Width;
            }

            var squareOfFramingCircle = Math.PI * radius * radius;
            (squareOfRectangles / squareOfFramingCircle).Should().BeInRange(0.6, 1);
        }

        [Test]
        public void PutNextRectangle_PlaceRectangleOnSpiral()
        {
            var circularCloudLayouter = new SpiralCloudLayouter(imageSettings);
            var centerX = imageSettings.Width / 2;
            var centerY = imageSettings.Height / 2;
            var coordinates = new[]
            {
                new Point(centerX, centerY), new Point(centerX, centerY + 1), new Point(centerX - 1, centerY + 1),
                new Point(centerX - 1, centerY), new Point(centerX - 2, centerY), new Point(centerX - 2, centerY - 1),
                new Point(centerX - 1, centerY - 2), new Point(centerX - 1, centerY - 3),
                new Point(centerX, centerY - 3),
                new Point(centerX + 1, centerY - 3), new Point(centerX + 2, centerY - 3),
                new Point(centerX + 3, centerY - 2)
            };
            foreach (var pointOnSpiral in coordinates)
            {
                var rectangle = circularCloudLayouter.PutNextRectangle(new Size(1, 1));
                rectangle.Location.GetDistanceTo(pointOnSpiral).Should()
                    .BeLessOrEqualTo(1.05 * rectangle.GetDiagonal());
            }
        }

        private double GetRadiusOfFramingCircle(Point center, Rectangle rectangle)
        {
            var distanceBetweenCenterAndLeftTopAngle = center.GetDistanceTo(rectangle.Location);
            var distanceBetweenCenterAndLeftBottomAngle = center.GetDistanceTo(new Point(rectangle.Left,
                rectangle.Bottom));
            var distanceBetweenCenterAndRightBottomAngle = center.GetDistanceTo(new Point(rectangle.Right,
                rectangle.Bottom));
            var distanceBetweenCenterAndRightTopAngle =
                center.GetDistanceTo(new Point(rectangle.Right,
                    rectangle.Top));
            return Math.Max(
                Math.Max(distanceBetweenCenterAndLeftTopAngle, distanceBetweenCenterAndLeftBottomAngle),
                Math.Max(distanceBetweenCenterAndRightBottomAngle, distanceBetweenCenterAndRightTopAngle));
        }
    }
}