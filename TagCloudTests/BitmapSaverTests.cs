using System;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.BitmapSaver;

namespace TagCloudTests
{
    public class BitmapSaverTests
    {
        [Test]
        public void Save_ShouldThrow_WhenIncorrectFormat()
        {
            Action act = () => BitmapSaver.Save(new Bitmap(100,100), "jpga");

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Save_ShouldThrow_WhenIncorrectPath()
        {
            Action act = () => BitmapSaver.Save(new Bitmap(100, 100), "png", "blablapath");

            act.Should().Throw<ArgumentException>();
        }
    }
}
