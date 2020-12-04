using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Common;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudTests.TextAnalyzingTests
{
    internal class TextAnalyzerTests
    {
        private string boringWords;
        private FilesSettings filesSettings;

        [SetUp]
        public void SetUp()
        {
            filesSettings = new FilesSettings();
            boringWords = "b, d";
            filesSettings.BoringWordsFilePath = @"..\..\boring words.txt";
        }

        [Test]
        public void GetWordsWithFrequency_FromLine()
        {
            var text = "a, b; c\na a b. d";
            var actual = new TextAnalyzer(text, boringWords).GetWordWithFrequency();
            var expected = new Dictionary<string, int> {{"a", 3}, {"c", 1}};
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromEmptyLine()
        {
            var text = "";
            var actual = new TextAnalyzer(text, boringWords).GetWordWithFrequency();
            var expected = new Dictionary<string, int>();
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromTxtFile()
        {
            filesSettings.TextFilePath = @"..\..\testTextAnalyzer.txt";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int> {{"a", 3}, {"c", 1}};
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromEmptyTxtFile()
        {
            filesSettings.TextFilePath = @"..\..\testEmptyTextAnalyzer.txt";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int>();
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromDocFile()
        {
            filesSettings.TextFilePath = @"..\..\testTextAnalyzer.doc";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int> {{"a", 3}, {"c", 1}};
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromEmptyDocFile()
        {
            filesSettings.TextFilePath = @"..\..\testEmptyTextAnalyzer.doc";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int>();
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromDocxFile()
        {
            filesSettings.TextFilePath = @"..\..\testTextAnalyzer.docx";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int> {{"a", 3}, {"c", 1}};
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordsWithFrequency_FromEmptyDocxFile()
        {
            filesSettings.TextFilePath = @"..\..\testEmptyTextAnalyzer.docx";
            var actual = new TextAnalyzer(filesSettings).GetWordWithFrequency();
            var expected = new Dictionary<string, int>();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}