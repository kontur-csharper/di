using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloudTests
{
    [TestFixture]
    internal class TagsCloudDrawerTests
    {
        private readonly TagsCloudSettings defaultSettings = TagsCloudSettings.DefaultSettings;

        [Test]
        public void DrawTagsCloud_ShouldThrow_IfCollectionIsNull()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            var graphics =
                Graphics.FromImage(new Bitmap(defaultSettings.ImageSize.Width, defaultSettings.ImageSize.Height));
            Action action = () => drawer.DrawTagsCloud(null, PointF.Empty, graphics);
            action.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void GetTagsCloud_ShouldThrow_IfCollectionIsNull()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            Action action = () => drawer.GetTagsCloud(null);
            action.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void GetTagsCloud_ShouldFormImage_WithSetSize()
        {
            var drawer = new TagsCloudDrawer(new RectanglesLayouter(Point.Empty), TagsCloudSettings.DefaultSettings);
            var tagsCloud = drawer.GetTagsCloud(new Word[0]);
            tagsCloud.Height.Should().Be(defaultSettings.ImageSize.Height);
            tagsCloud.Width.Should().Be(defaultSettings.ImageSize.Width);
        }
    }
}