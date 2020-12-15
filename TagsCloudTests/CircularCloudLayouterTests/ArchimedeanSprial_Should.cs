using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Core.CircularCloudLayouter;

namespace TagsCloudTests.CircularCloudLayouterTests
{
    [TestFixture]
    public class ArchimedeanSpiralShould
    {
        private readonly CloudLayouterSettings cloudLayouterSettings = new CloudLayouterSettings();
        
        [TestCase(0, 0, TestName = "EmptyPoint")]
        [TestCase(1, 2, TestName = "PointWithPositiveCoords")]
        [TestCase(-1, -2, TestName = "PointWithNegativeCoords")]
        [TestCase(1, -2, TestName = "PointWithMixedCoords")]
        public void ArchimedeanSpiralCtor_OnAllPoints_DoesNotThrow(int x, int y)
        {
            Action callCtor = () => _ = new ArchimedeanSpiral(cloudLayouterSettings);
            callCtor.Should().NotThrow();
        }

        [Test]
        public void GetNextPoint_OnEqualSpiralInstance_ShouldBeEqual()
        {
            var firstSpiral = new ArchimedeanSpiral(cloudLayouterSettings);
            var secondSpiral = new ArchimedeanSpiral(cloudLayouterSettings);

            var firstPoints = Enumerable.Range(0, 50).Select(i => firstSpiral.GetNextPoint());
            var secondPoints = Enumerable.Range(0, 50).Select(i => secondSpiral.GetNextPoint());
            firstPoints.Should().BeEquivalentTo(secondPoints);
        }

        [Test]
        public void GetNextPoint_OnDifferentSpiralInstance_ShouldBeDifferent()
        {
            var firstSpiral = new ArchimedeanSpiral(cloudLayouterSettings);
            var secondSpiral = new ArchimedeanSpiral(new CloudLayouterSettings { StartPoint = new Point(100, 100)});

            var firstPoints = Enumerable.Range(0, 50).Select(i => firstSpiral.GetNextPoint());
            var secondPoints = Enumerable.Range(0, 50).Select(i => secondSpiral.GetNextPoint());
            firstPoints.Should().NotBeEquivalentTo(secondPoints);
        }
    }
}