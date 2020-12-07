using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Spirals;

namespace TagsCloud.Tests
{
    internal class ArchimedeanSpiral_Tests
    {
        private ArchimedeanSpiral spiral;

        [SetUp]
        public void SetUp()
        {
            spiral = new ArchimedeanSpiral(new Point(), 0.1);
        }

        [Test]
        public void InitializeSpiral_ThrowException_WhenNotPositiveSpiralParameter()
        {
            Assert.Throws<ArgumentException>(() => spiral = new ArchimedeanSpiral(new Point(), -1));
        }

        [Test]
        public void GetNextPoint_FirstPointEqualsCenter()
        {
            var center = new Point(-1, 2);
            spiral = new ArchimedeanSpiral(center, 0.1);
            spiral.GetNextPoint().Should().Be(center);
        }

        [TestCase(10, TestName = "WhenGet10Point")]
        [TestCase(100, TestName = "WhenGet100Point")]
        [TestCase(1000, TestName = "WhenGet1000Point")]
        public void GetNextPoint_AllPointsShouldBeDifferent(int count)
        {
            var points = Enumerable.Range(0, count).Select(_ => spiral.GetNextPoint()).ToList();

            foreach (var point in points)
                points.Where(x => x != point).Any(x => x == point).Should().BeFalse();
        }
    }
}