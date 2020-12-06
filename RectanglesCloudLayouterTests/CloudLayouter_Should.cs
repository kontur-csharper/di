using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using RectanglesCloudLayouter.Core;
using RectanglesCloudLayouter.Interfaces;
using RectanglesCloudLayouter.RectanglesCloudVisualization;
using RectanglesCloudLayouter.SpecialMethods;

namespace RectanglesCloudLayouterTests
{
    public class CloudLayouterShould
    {
        private CloudLayouter _sut;
        private List<Rectangle> _rectangles;
        private ISpiral _spiral;
        private ICloudRadiusCalculator _cloudRadiusCalculator;

        [SetUp]
        public void SetUp()
        {
            _spiral = new ArchimedeanSpiral(new Point(0, 0), new SpiralSettings(1, 0.5));
            _cloudRadiusCalculator = new CloudRadiusCalculator();
            _sut = new CloudLayouter(_spiral, _cloudRadiusCalculator);
            _rectangles = new List<Rectangle>();
        }

        [TearDown]
        public void TearDown()
        {
            if (!TestContext.CurrentContext.Result.Outcome.Status.Equals(TestStatus.Failed) ||
                _rectangles.Count == 0) return;
            var test = TestContext.CurrentContext.Test;
            var testName = test.FullName.Split('.');
            var fullTestName =
                $"{testName[0]}.{testName[1]}.{test.MethodName}.{(testName.Length == 3 ? testName[2] : "")}";
            var imageName = $"{DateTime.Now:yyyy.MM.dd HH.mm.ss}_{fullTestName}_failed.bmp";
            CircularCloudVisualisation.MakeImageCloud(_rectangles, _cloudRadiusCalculator.CloudRadius, imageName,
                ImageFormat.Bmp);
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        public void PutNextRectangle_ThrowArgumentException_WhenSizeParametersNotPositive(int width, int height)
        {
            var size = new Size(width, height);

            var act = new Action(() => _rectangles.Add(_sut.PutNextRectangle(size)));

            act.Should().Throw<ArgumentException>();
        }

        [TestCase(1, 1)]
        public void PutNextRectangle_DoNotThrowArgumentException_WhenSimplePositiveSize(int width, int height)
        {
            var size = new Size(width, height);

            var act = new Action(() => _rectangles.Add(_sut.PutNextRectangle(size)));

            act.Should().NotThrow<ArgumentException>();
        }

        [TestCase(10, 5)]
        [TestCase(3, 7)]
        [TestCase(23, 23)]
        public void
            PutNextRectangle_LocationOfTheRectangleCenterIsEquivalentToSpiralCenterPosition_WhenPutFirstRectangleAnySize(
                int widthRectangle, int heightRectangle)
        {
            var size = new Size(widthRectangle, heightRectangle);

            _rectangles.Add(_sut.PutNextRectangle(size));

            _sut.GetCurrentRectangle.Location.Should().Be(_spiral.Center - size / 2);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(10)]
        [TestCase(0)]
        public void PutNextRectangle_SameCountOfRectangles_WhenPutCertainCountOfRectangles(int countRectangles)
        {
            _rectangles.AddRange(_sut.MakeLayouter(countRectangles, 50,
                70, 20, 40));

            _sut.RectanglesCount.Should().Be(countRectangles);
        }


        [Test, Timeout(1000)]
        public void PutNextRectangle_MakeItFastUntilTimeEnd_WhenPutOneThousandRectangles()
        {
            _rectangles.AddRange(_sut.MakeLayouter(1000, 50,
                70, 20, 40));
        }

        [Test]
        public void PutNextRectangle_IncreasedCloudRadius_WhenPutFirstRectangle()
        {
            var size = new Size(5, 5);

            _rectangles.Add(_sut.PutNextRectangle(size));

            _cloudRadiusCalculator.CloudRadius.Should().Be(5);
        }
    }
}