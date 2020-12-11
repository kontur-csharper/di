using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.Visualizer;

namespace TagCloud.Tests
{
    public class Tests
    {
        private Point center;
        private CircularCloudLayouter cloudLayouter;
        private List<RectangleWithWord> resultRectsWithWord;
        private SizeWithWord[] sizes;

        [SetUp]
        public void SetUp()
        {
            cloudLayouter = new CircularCloudLayouter(new CenterOptions(center.X, center.Y));
            sizes = SizesCreator.CreateSizesArray(new[] {"abc", "bca", "cab"}, 12f, "Times New Roman");
        }

        [Test]
        public void CircularCloudLayouter_IsFirstRectInCenter()
        {
            var size = new SizeWithWord(new Size(5, 5), new Word(default, default));
            resultRectsWithWord = cloudLayouter.GetRectangles(new[] {size});

            resultRectsWithWord[0].Rectangle.Location.Should().Be(center);
        }

        [Test]
        public void PutNextRectangle_RectanglesDoNotIntersect_AfterAddition()
        {
            resultRectsWithWord = cloudLayouter.GetRectangles(sizes);

            cloudLayouter.Rectangles.Any(r => r.IntersectsWith(cloudLayouter.Rectangles.Except(new[] {r}))).Should()
                .BeFalse();
        }

        [Test]
        public void PutNextRectangle_PutRectanglesInCircleForm_AfterAddition()
        {
            const int expectedRadius = 150;

            cloudLayouter.GetRectangles(sizes);
            var mostTopRect = cloudLayouter.Rectangles.OrderBy(rect => rect.Top).First();
            var mostDownRect = cloudLayouter.Rectangles.OrderByDescending(rect => rect.Bottom).First();
            var mostLeftRect = cloudLayouter.Rectangles.OrderBy(rect => rect.Left).First();
            var mostRightRect = cloudLayouter.Rectangles.OrderByDescending(rect => rect.Right).First();
            var dy = Math.Max(center.Y - mostTopRect.Top, mostDownRect.Bottom - center.Y);
            var dx = Math.Max(center.X - mostLeftRect.Left, mostRightRect.X - center.X);
            var difBetweenDeltas = Math.Abs(dx - dy);

            dy.Should().BeLessThan(expectedRadius);
            dx.Should().BeLessThan(expectedRadius);
            difBetweenDeltas.Should().BeLessThan(expectedRadius / 2);
        }

        [TearDown]
        public void TearDown()
        {
            var testResult = TestContext.CurrentContext.Result.Outcome;
            if (testResult != ResultState.Failure)
            {
                return;
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(),
                "..",
                "..",
                "..",
                "crash-reports");
            var bitmap = BitmapCreator.DrawBitmap(resultRectsWithWord, new ImageOptions());
            bitmap.Save(Path.Combine(path, $"crash-report {TestContext.CurrentContext.Test.FullName}.bmp"));
            Console.WriteLine($"Tag cloud visualization saved to file {path}");
        }
    }
}