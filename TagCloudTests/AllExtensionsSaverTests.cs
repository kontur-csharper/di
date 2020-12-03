using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.ImageSavers;

namespace TagCloudTests
{
    public class AllExtensionsSaverTests
    {
        private AllExtensionsSaver saver;

        [SetUp]
        public void SetUp()
        {
            saver = new AllExtensionsSaver();
        }

        [Test]
        public void Save_ShouldThrowArgumentException_WhenUnsupportedImageFormat()
        {
            Assert.Throws<ArgumentException>(
                () => saver.Save(new Bitmap(100, 100), "aa/bb", "abcdxyzw"));
        }

        [TestCase("jpeg", TestName = "jpeg")]
        [TestCase("jpg", TestName = "jpg")]
        [TestCase("png", TestName = "png")]
        [TestCase("gif", TestName = "gif")]
        [TestCase("bmp", TestName = "bmp")]
        [TestCase("tif", TestName = "tif")]
        public void Save_ShouldSaveImagesInDifferentFormats(string format)
        {
            const string filename = "image";
            using var image = new Bitmap(100, 100);
            saver.Save(image, filename, format);
            var imagePath = $"{filename}.{format}";

            File.Exists(imagePath).Should().BeTrue();

            File.Delete(imagePath);
        }
    }
}