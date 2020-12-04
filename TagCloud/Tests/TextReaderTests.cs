using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextProcessing;

namespace TagCloud.Tests
{
    public class TextReaderTests
    {
        private PathCreater creator = new PathCreater();
        
        [Test]
        public void TxtReaderTest()
        {
            var txtReader = new TxtTextReader();
            txtReader.ReadStrings(creator.GetPathToFile("input.txt"))
                .Should().HaveCount(24)
                .And.Contain("cat")
                .And.Contain("кошка")
                .And.Contain("крокодил");
        }

        [Test]
        public void DocxReaderTest()
        {
            var docxReader = new DocxTextReader();
            docxReader.ReadStrings(creator.GetPathToFile("input.docx"))
                .Where(word => word != "")
                .Should().HaveCount(24)
                .And.Contain("cat")
                .And.Contain("кошка")
                .And.Contain("крокодил");
        }
    }
}