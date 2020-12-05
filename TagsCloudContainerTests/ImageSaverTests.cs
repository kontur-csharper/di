using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class ImageSaverTests
    {
        [Test]
        public void Ctor_IfImageFormatIsNotSupported_ThrowArgumentException()
        {
            Action act = () => new ImageSaver("image.blabla");

            act.Should().Throw<ArgumentException>();
        }

        [TestCase("image.png", TestName = "WithPng")]
        [TestCase("image.gif", TestName = "WithGif")]
        [TestCase("image.bmp", TestName = "WithBmp")]
        [TestCase("image.jpg", TestName = "WithJpg")]
        [TestCase("image.tif", TestName = "WithTiff")]
        public void Save_CorrectWorkWithDifferentFormat(string imagePath)
        {
            var image = new Bitmap(100, 100);
            var saver = new ImageSaver(imagePath);
            
            saver.Save(image);

            File.Exists(imagePath).Should().BeTrue();
            
            File.Delete(imagePath);
        }
    }
}