using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloud.Tests
{
    public class CanvasTest
    {
        [Test]
        public void Canvas_HasCorrectCenter()
        {
            var canvas = new Canvas(400, 600);
            canvas.Center.Should().BeEquivalentTo(new Point(200, 300));
        }
    }
}