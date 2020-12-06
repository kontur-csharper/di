using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.PointsGenerators;

namespace TagsCloudVisualizationTests.PointsGeneratorsTests
{
    [TestFixture]
    public class ArchimedesSpiralTests
    {
        [SetUp]
        public void SetUp()
        {
            spiralParams = new SpiralParams();
            canvas = new Canvas(new ImageSettings {Width = 500, Height = 500});
            sut = new ArchimedesSpiral(spiralParams, canvas.GetImageCenter());
        }

        private IPointGenerator sut;
        private SpiralParams spiralParams;
        private ICanvas canvas;

        [TestCase(0)]
        [TestCase(0.0000f)]
        [TestCase(-0.0000000f)]
        [TestCase(0.5f, 0)]
        public void InitArchimedesSpiral_Throws_IncorrectArguments(float angleStep,
            int spiralParameter = 1)
        {
            var spiralParams = new SpiralParams(spiralParameter, angleStep);
            Assert.Throws<ArgumentException>(
                () => new ArchimedesSpiral(spiralParams, canvas.GetImageCenter()));
        }

        [Test]
        public void StartOverPointGenerator_ShouldResetGeneration()
        {
            var expectedPoints = new Point[3];
            for (int i = 0; i < 3; i++)
                expectedPoints[i] = sut.GetNextPoint();

            sut.StartOver();

            var actualPoints = new Point[3];
            for (int i = 0; i < 3; i++)
                actualPoints[i] = sut.GetNextPoint();

            actualPoints.Should().Equal(expectedPoints);
        }

        [TestCase(2, 0.2f, 250, 250, 252, 252, 250, 252)]
        [TestCase(-2, 0.2f, 250, 250, 248, 248, 250, 248)]
        [TestCase(2, -1f, 250, 250, 248, 252, 252, 254)]
        [TestCase(-2, -1f, 250, 250, 252, 248, 248, 246)]
        public void GetNextGeneratedPoint_ShouldConsistentlyReturnsNextSpiralPoints(int spiralParameter, float angleStep,
            params int[] expectedPointCoordinates)
        {
            var spiralParams = new SpiralParams(spiralParameter, angleStep);
            var archimedesSpiral = new ArchimedesSpiral(spiralParams, canvas.GetImageCenter());
            var expectedPoints = new Point[3];
            for (int i = 0; i < expectedPoints.Length; i++)
                expectedPoints[i] = new Point(expectedPointCoordinates[i * 2], expectedPointCoordinates[i * 2 + 1]);
        
            var actualPoints = new Point[3];
            for (int i = 0; i < 3; i++)
                actualPoints[i] = archimedesSpiral.GetNextPoint();
        
            actualPoints.Should().Equal(expectedPoints);
        }
    }
}