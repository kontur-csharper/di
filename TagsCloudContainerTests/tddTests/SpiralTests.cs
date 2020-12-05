using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class SpiralTests
    {
        private Spiral spiral;
        private List<Point> points;

        [SetUp]
        public void SetUp()
        {
            spiral = new Spiral();
            points = new List<Point>();
        }

        [Test]
        public void GetNextPoint_FirstCall_ReturnEmptyPoint()
        {
            var point = spiral.GetNextPoint();

            point.Should().Be(Point.Empty);
        }

        [Test]
        public void GetNextPoint_AllPointsAreUnique()
        {
            for (int i = 0; i < 100; i++)
            {
                points.Add(spiral.GetNextPoint());
            }

            points.Should().OnlyHaveUniqueItems();
        }

        [Test]
        public void GetNextPoint_EveryNextPointMoreDistantFromCenter()
        {
            for (int i = 0; i < 100; i++)
            {
                points.Add(spiral.GetNextPoint());
            }

            points.Should().BeInAscendingOrder(new PointComparerByDistance());
        }

        [Test]
        public void GetNextPoint_LineFromCenterToPointHasAngleMultipleAngleStep()
        {
            var angleStepInDegrees = 45;
            spiral = new Spiral(Math.PI / 4);

            for (int i = 0; i < 100; i++)
            {
                points.Add(spiral.GetNextPoint());
            }

            points.Skip(1).Should().OnlyContain(point =>
                GetAngleInDegrees(point) % angleStepInDegrees == 0);
        }

        private int GetAngleInDegrees(Point point)
        {
            var angle = Math.Asin(
                point.Y / PointComparerByDistance.CalculatePointDistance(point));
            return (int) Math.Round(angle * 180 / Math.PI);
        }

        private class PointComparerByDistance : IComparer<Point>
        {
            public int Compare(Point first, Point second)
            {
                var firstDistance = CalculatePointDistance(first);
                var secondDistance = CalculatePointDistance(second);

                if (Math.Abs(firstDistance - secondDistance) < 0.05)
                {
                    return 0;
                }

                return firstDistance > secondDistance ? 1 : -1;
            }

            public static double CalculatePointDistance(Point point)
            {
                return Math.Sqrt(point.X * point.X + point.Y * point.Y);
            }
        }
    }
}