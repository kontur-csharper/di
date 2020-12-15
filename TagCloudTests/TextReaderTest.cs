using System.IO;
using NUnit.Framework;
using TagCloud.TextConverters.TextReaders;

namespace TagCloudTests
{
    [TestFixture]
    class TextReaderTest
    {
        private readonly TextReaderTxt reader = new TextReaderTxt();
        private readonly string path = $".{Path.DirectorySeparatorChar}fileTest.txt";

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("as dsa")]
        [TestCase("hjkl\nasdf\n\nsadf asdf fda")]
        public void WriteTextAndRead_ShouldBeEquals(string text)
        {
            File.WriteAllText(path, text);
            var result = reader.ReadText(path);
            Assert.AreEqual(text, result);
            File.Delete(path);
        }

        [Test]
        public void ReadTextFromFileDoesntExist_ShouldBeNull()
        {
            var result = reader.ReadText(path);
            Assert.IsNull(result);
        }
    }
}
