using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class Utils_Should
    {
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0, 1)]
        [TestCase(0, 1, 0, 0, 1)]
        [TestCase(0, 0, 1, 0, 1)]
        [TestCase(0, 0, 0, 1, 1)]
        [TestCase(3, 4, 0, 0, 5)]
        [TestCase(1, 2, -2, -2, 5)]
        [TestCase(1, 0, -2, 0, 3)]
        [TestCase(1, 0, 1, 0, 0)]
        [TestCase(1, 0, 2, 0, 1)]
        [TestCase(0, 1, 0, -2, 3)]
        [TestCase(0, 1, 0, 1, 0)]
        [TestCase(0, 1, 0, 2, 1)]
        public void Test(int x1, int y1, int x2, int y2, double expected)
        {
            Utils.GetDistance(new Point(x1, y1), new Point(x2, y2)).Should().Be(expected);
        }
    }
}