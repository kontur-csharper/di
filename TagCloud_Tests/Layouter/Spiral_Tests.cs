using System;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using TagCloud.Layouters;

namespace TagsCloud_Tests.Layouter
{
    [TestFixture]
    public class Spiral_Tests
    {
        [Test]
        public void Spiral_ShouldStartInCenter()
        {
            var center = new Point(10, 10);
            var spiral = new Spiral(center, 1, 1);
            spiral.GetNextPoint().Should().BeEquivalentTo(center);
        }
        
        [Test]
        public void Spiral_ShouldIncreaseDistanceFromCenter_AfterSomeSteps()
        {
            var center = new Point(0,0);
            var spiral = new Spiral(center, 100, 0.05 * Math.PI);
            
            var prevDistance = GetDistance(spiral.GetNextPoint(), center);
            
            for (var i = 0; i < 100; i++)
            {
                var newDistance = GetDistance(spiral.GetNextPoint(), center);
                newDistance.Should().BeGreaterThan(prevDistance);
                prevDistance = newDistance;
            }
        }
        
        
        [Test]
        public void GetQuadrant_ShouldReturnCorrectQuadrant_AfterSomeSteps()
        {
            var center = new Point(0,0);
            var spiral = new Spiral(center, 100, 0.25 * Math.PI);

            spiral.GetNextPoint();
            spiral.GetNextPoint();
            spiral.Quadrant.Should().BeEquivalentTo(Quadrant.Top);
            spiral.GetNextPoint();
            spiral.GetNextPoint();
            spiral.Quadrant.Should().BeEquivalentTo(Quadrant.Left);
            spiral.GetNextPoint();
            spiral.GetNextPoint();
            spiral.Quadrant.Should().BeEquivalentTo(Quadrant.Bottom);
            spiral.GetNextPoint();
            spiral.GetNextPoint();
            spiral.Quadrant.Should().BeEquivalentTo(Quadrant.Right);
            spiral.GetNextPoint();
            spiral.GetNextPoint();
            spiral.Quadrant.Should().BeEquivalentTo(Quadrant.Top);
            
        }
        
        [Test]
        public void GetNextPoint_ShouldReturnCorrectPoints_AfterSomeSteps()
        {
            var center = new Point(0,0);
            var spiral = new Spiral(center, 4, 0.5 * Math.PI);
            spiral.GetNextPoint().Should().BeEquivalentTo(new Point(0,0));
            spiral.GetNextPoint().Should().BeEquivalentTo(new Point(0,1));
            spiral.GetNextPoint().Should().BeEquivalentTo(new Point(-2,0));
            spiral.GetNextPoint().Should().BeEquivalentTo(new Point(0,-3));
            spiral.GetNextPoint().Should().BeEquivalentTo(new Point(4,0));
        }

        private double GetDistance(Point first, Point second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        }
    }
}