using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using TagsCloud.Layouter;

namespace TagsCloudTest
{
    public class TargetVectorTests
    {
        private TargetVector vector;
        private Point target;
        private Point location;

        [SetUp]
        public void SetUp()
        {
            target = new Point(2, 3);
            location = new Point(5, 7);
            vector = new TargetVector(target, location);
        }

        [Test]
        public void PartialDeltaReturnMinimalOffset()
        {
            var minimalOffset = new[] { 1, 0, -1 };

            foreach (var delta in vector.GetPartialDelta())
            {
                minimalOffset.Should().Contain(delta.X);
                minimalOffset.Should().Contain(delta.Y);
            }
        }

        [Test]
        public void PartialDeltaAllDeltaMoveToTarget()
        {
            var dx = 0;
            var dy = 0;

            foreach (var delta in vector.GetPartialDelta())
            {
                dx += delta.X;
                dy += delta.Y;
            }
            location.Offset(dx, dy);

            location.Should().Be(target);
        }

    }
}
