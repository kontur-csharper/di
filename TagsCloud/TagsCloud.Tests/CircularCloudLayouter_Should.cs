using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Infrastructure;
using TagsCloud.Layouters;

namespace TagsCloud.Tests
{
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;

        [SetUp]
        public void SetUp()
        {
            layouter = new CircularCloudLayouter(new ImageSettings(1080, 1080));
        }

        [Test]
        public void HaveCentralPoint_OnCreation()
        {
            layouter.GetCenterPoint()
                .Should().Be(new Point(540, 540));
        }

        [Test]
        public void ReturnRectangle_WithSameSize_WhenCalled_PutNextRectangle()
        {
            layouter.PutNextRectangle(new Size(20, 10)).Size
                .Should().BeEquivalentTo(new Size(20, 10));
        }

        [Test]
        public void ContainAllAddedRectangles()
        {
            var sizes = new List<Size>
                {new Size(1, 1), new Size(2, 2)};
            var rectangles = sizes.Select(size => layouter.PutNextRectangle(size));

            rectangles.Select(x => x.Size)
                .Should().BeEquivalentTo(sizes);
        }

        [Test]
        public void NotContainIntersectedRectangles()
        {
            var rects = AddRectanglesToLayouter(10, new Size(100, 30));
            rects.AddRange(AddRectanglesToLayouter(30, new Size(70, 20)));

            rects
                .Should().OnlyContain(rect =>
                    rects.All(y => !y.IntersectsWith(rect) || y == rect));
        }

        [Test]
        [Timeout(10000)]
        public void BeEfficient_WithManyWords()
        {
            AddRectanglesToLayouter(1650, new Size(1, 1));
        }

        private List<Rectangle> AddRectanglesToLayouter(int amount, Size size)
        {
            var rects = new List<Rectangle>();
            for (var i = 0; i < amount; i++)
                rects.Add(layouter.PutNextRectangle(size));
            return rects;
        }
    }
}