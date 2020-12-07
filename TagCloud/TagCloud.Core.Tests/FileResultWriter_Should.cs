using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Output;

namespace TagCloud.Core.Tests
{
    // ReSharper disable once InconsistentNaming
    public class FileResultWriter_Should
    {
        private string temporaryFilePath;
        private FileResultWriter writer;
        private Image sample;

        private static readonly TestCaseData[] formatTestCases =
            new[] {ImageFormat.Gif, ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Tiff, ImageFormat.Bmp}
                .Select(x => new TestCaseData(x) {TestName = x.ToString()})
                .ToArray();

        [SetUp]
        public void SetUp()
        {
            sample = CreateSampleImage();
            writer = new FileResultWriter();
            temporaryFilePath = GetFilePath(".test");
        }

        [TearDown]
        public void TearDown()
        {
            sample?.Dispose();
            if(!string.IsNullOrEmpty(temporaryFilePath))
                File.Delete(temporaryFilePath);
        }

        [Test]
        public void SaveImage_AtPassedPath()
        {
            writer.Save(sample, ImageFormat.Png, temporaryFilePath);

            Assert.True(File.Exists(temporaryFilePath));
        }

        [TestCaseSource(nameof(formatTestCases))]
        public void SaveImage_WithProvidedFormat(ImageFormat format)
        {
            writer.Save(sample, format, temporaryFilePath);

            using var saved = Image.FromFile(temporaryFilePath);
            saved.RawFormat.Should().Be(format);
        }

        private Image CreateSampleImage()
        {
            var image = new Bitmap(100, 200);
            using var g = Graphics.FromImage(image);
            g.DrawRectangle(Pens.Red, new Rectangle(0, 0, 100, 200));
            return image;
        }

        private string GetFilePath(string extension) =>
            Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                $"{TestContext.CurrentContext.Test.MethodName}.{extension}"
            );
    }
}