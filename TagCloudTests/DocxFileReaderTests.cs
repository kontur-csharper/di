using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.FileReaders;

namespace TagCloudTests
{
    public class DocxFileReaderTests
    {
        private DocxFileReader reader;
        [SetUp]
        public void SetUp()
        {
            reader = new DocxFileReader();
        }

        [Test]
        public void GetWordsFromFile_ShouldWorksCorrect_WhenReadWordsFromFile()
        {
            var reader = new DocxFileReader();

            var filePath = Path.Combine(Environment.CurrentDirectory, "TestsResourses\\file2.docx");

            var result = reader.GetWordsFromFile(filePath);

            result.Should().BeEquivalentTo("Всем", "Привет", "Это", "Данил");
        }

        [Test]
        public void GetWordsFromFile_ShouldThrow_WhenFileDoesNotExists()
        {
            Action act = () => reader.GetWordsFromFile("bla bla file");

            act.Should().Throw<ArgumentException>();
        }
    }
}
