using System.IO;
using System.Linq;
using FluentAssertions;
using GemBox.Document;
using NUnit.Framework;
using TagCloud.TextFileParser;
using TagCloud.Visualizer.Console;
using TextReader = TagCloud.Visualizer.Console.TextReader;

namespace TagCloud.Tests
{
    [TestFixture]
    public class TextReaderTests
    {
        private static readonly string SourcePath = Path.Combine(Directory.GetCurrentDirectory(),
            "test-files");

        private static readonly TxtFileParser TxtFileParser = new TxtFileParser();
        private static readonly WordDocumentParser WordDocumentParser = new WordDocumentParser();
        private static readonly ToLowerCaseProcessor ToLowerCaseProcessor = new ToLowerCaseProcessor();

        [OneTimeSetUp]
        public void SetUp()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        [Test]
        public void GetWords_ExcludeBoringWords()
        {
            var words = TextReader.GetWords(new InputOptions("test.txt"),
                SourcePath,
                TxtFileParser,
                ToLowerCaseProcessor);

            words.Should().NotContain("это");
        }

        [Test]
        public void GetWords_ReadLinesFromTxtDocument()
        {
            var words = TextReader.GetWords(new InputOptions("test.txt"),
                    SourcePath,
                    TxtFileParser,
                    ToLowerCaseProcessor)
                .ToArray();

            words.Should().Contain(new[] {"большой", "средний", "маленький"});
            words.Should().HaveCount(6);
        }

        [Test]
        public void GetWords_ReadLinesFromDocxDocument()
        {
            var words = TextReader.GetWords(new InputOptions("test.docx"),
                    SourcePath,
                    WordDocumentParser,
                    ToLowerCaseProcessor)
                .ToArray();

            words.Should().Contain(new[] {"большой", "средний", "маленький"});
            words.Should().HaveCount(6);
        }
    }
}