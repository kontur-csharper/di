using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudTags;
using TagsCloudVisualization.Configs;

namespace TagsCloudVisualization_Should
{
    public class DrawerShould
    {
        private IConfig config;

        [SetUp]
        public void SetUp()
        {
            config = new Config();
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                new Point(1500, 1500), Color.Blue, new Size(1500, 1500), new HashSet<string>());
        }

        [Test]
        public void DrawImage_ThrowArgumentException_CenterWithNegativeXOrY()
        {
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                new Point(-1, -1), Color.Blue, new Size(1500, 1500), new HashSet<string>());
            var cloudTags = new List<ICloudTag> {new CloudTag(new Rectangle(1, 1, 1, 1), "")};

            Action act = () => Drawer.DrawImage(cloudTags, config);

            act.ShouldThrow<ArgumentException>().WithMessage("X or Y of center was negative");
        }

        [Test]
        public void DrawImage_ThrowArgumentException_SequenceOfElementsIsEmpty()
        {
            Action act = () => Drawer.DrawImage(new List<ICloudTag>(), config);

            act.ShouldThrow<ArgumentException>().WithMessage("The sequence contains no elements");
        }

        [Test]
        public void DrawImage_CorrectImageSize_TenRectangles()
        {
            var rectangles = GetRectangles(10);
            config.SetValues(new Font(FontFamily.GenericMonospace, 25),
                new Point(500, 500), Color.Blue, new Size(1500, 1500), new HashSet<string>());
            var expectedSize = new Size(1500, 1500);
            var cloutTags = rectangles.Select(x => new CloudTag(x, "hello"))
                .ToList().Cast<ICloudTag>().ToList();

            var actualBitmap = Drawer.DrawImage(cloutTags, config);

            actualBitmap.Size.Should().Be(expectedSize);
        }


        [Test]
        public void DrawImage_CorrectRawFormat_HundredRectangles()
        {
            var rectangles = GetRectangles(100);
            var cloutTags = rectangles.Select(x => new CloudTag(x, "hello"))
                .ToList().Cast<ICloudTag>().ToList();
            var expectedFormat = ImageFormat.MemoryBmp;

            var actualBitmap = Drawer.DrawImage(cloutTags, config);

            actualBitmap.RawFormat.Should().Be(expectedFormat);
        }

        private List<Rectangle> GetRectangles(int count)
        {
            var rectangles = new List<Rectangle>();

            for (var i = 0; i < count; i++) rectangles.Add(new Rectangle(i, i, i, i));

            return rectangles;
        }
    }
}