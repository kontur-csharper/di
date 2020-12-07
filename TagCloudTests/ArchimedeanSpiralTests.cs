using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.PointGenerator;

namespace TagCloudTests
{
    public class ArchimedeanSpiralTests
    {
        private ArchimedeanSpiral _spiral;

        [SetUp]
        public void SetUp()
        {
            _spiral = new ArchimedeanSpiral(new Point(5, 7));
        }

        [Test]
        public void GetNextPoint_ShouldReturnCenterPoint_WhenFirstCall()
        {
            var center = new Point(5, 7);

            _spiral.GetNextPoint().Should().BeEquivalentTo(center);
        }

        [Test]
        public void GetNextPoint_ShouldNotContainsDuplicatePoints_WhenMoreThanOneCall()
        {
            var pointsFromSpiral = new List<Point>();

            for (var i = 0; i < 10; i++)
            {
                var point = _spiral.GetNextPoint();
                pointsFromSpiral.Add(point);
            }

            IsContainsDuplicatePoints().Should().BeFalse();


            bool IsContainsDuplicatePoints()
            {
                var set = new HashSet<Point>();
                return pointsFromSpiral.All(i => set.Add(i));
            }
        }
    }
}
