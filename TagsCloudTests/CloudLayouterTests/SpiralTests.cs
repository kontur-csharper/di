using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouter;

namespace TagsCloudTests.CloudLayouterTests
{
    internal class SpiralTests
    {
        private Spiral spiral;

        [SetUp]
        public void SetUp()
        {
            spiral = new Spiral(new Point(0, 0));
        }

        [Test]
        public void Ctor_ThrowsArgumentException_WhenCoefOfSpiralEquationNotPositive()
        {
            Assert.Throws<ArgumentException>(() => new Spiral(new Point(0, 0), 0, 1));
        }

        [Test]
        public void Ctor_ThrowsArgumentException_WhenDeltaOfAnglePhiNotPositive()
        {
            Assert.Throws<ArgumentException>(() => new Spiral(new Point(0, 0), 1, 0));
        }

        [Test]
        public void Ctor_Correct_WithAllCorrectParameters()
        {
            var spiral = new Spiral(new Point(0, 0), 1, 1);
            spiral.Center.Should().BeEquivalentTo(new Point(0, 0));
            spiral.CoefOfSpiralEquation.Should().Be(1);
            spiral.DeltaOfAnglePhi.Should().Be(1);
        }

        [Test]
        public void Ctor_Correct_WithOnlyParameterCenter()
        {
            var spiral = new Spiral(new Point(0, 0));
            spiral.Center.Should().BeEquivalentTo(new Point(0, 0));
            spiral.CoefOfSpiralEquation.Should().Be(0.5);
            spiral.DeltaOfAnglePhi.Should().Be(Math.PI / 90);
        }

        [Test]
        public void GetNextPointOnSpiral_ReturnCorrectCoordinates()
        {
            var coefSpiralEquation = 0.5;
            var anglePhi = 0.0;
            var deltaAnglePhi = Math.PI / 90;
            for (var i = 0; i < 100; i++)
            {
                var x = Math.Round(coefSpiralEquation * anglePhi * Math.Cos(anglePhi));
                var y = Math.Round(coefSpiralEquation * anglePhi * Math.Sin(anglePhi));
                spiral.GetNextPointOnSpiral().Should().BeEquivalentTo(new Point((int) x, (int) y));
                anglePhi += deltaAnglePhi;
            }
        }
    }
}