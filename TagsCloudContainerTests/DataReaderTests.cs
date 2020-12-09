using TagsCloudContainer.Infrastructure.DataReader;
using TagsCloudContainer.App.Settings;
using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.App.DataReader;

namespace TagsCloudContainerTests
{
    internal class DataReaderTests
    {
        private readonly string txtFilePath = Path.Combine(Directory.GetCurrentDirectory(),
            "..", "..", "..", "files", "input.txt");
        private readonly string docxFilePath = Path.Combine(Directory.GetCurrentDirectory(),
            "..", "..", "..", "files", "input.docx");
        private readonly IDataReaderFactory dataReaderFactory;
        private readonly InputSettings inputSettings;

        public DataReaderTests()
        {
            dataReaderFactory = new DataReaderFactory(InputSettings.Instance);
            inputSettings = InputSettings.Instance;
        }

        [Test]
        public void DataReaderFactory_ShouldCreateTxtReader()
        {
            DataReaderFactory_ShouldCreateReaderForFile(txtFilePath);
        }

        [Test]
        public void DataReaderFactory_ShouldCreateDocxReader()
        {
            DataReaderFactory_ShouldCreateReaderForFile(docxFilePath);
        }

        [Test]
        public void DataReaderFactory_ShouldThrowNotImplementedException_IfInputFileIsWithInvalidExtension()
        {
            inputSettings.InputFileName = "file.png";
            Func<IDataReader> func = () => dataReaderFactory.CreateDataReader();
            func.Should().Throw<NotImplementedException>();
        }

        [Test]
        public void TxtReader_ShouldReadLines()
        {
            Reader_ShouldReadLinesFromFile(txtFilePath, new[] { "Это", "Txt", "Файл" });
        }

        [Test]
        public void DocxReader_ShouldReadLines()
        {
            Reader_ShouldReadLinesFromFile(docxFilePath, new[] {"Это", "Docx", "Файл", ""});
        }

        private void Reader_ShouldReadLinesFromFile(string filePath, string[] lines)
        {
            inputSettings.InputFileName = filePath;
            var docxReader = dataReaderFactory.CreateDataReader();
            docxReader.ReadLines().ToArray().Should().BeEquivalentTo(lines);
        }

        private void DataReaderFactory_ShouldCreateReaderForFile(string filePath)
        {
            inputSettings.InputFileName = filePath;
            Func<IDataReader> func = () => dataReaderFactory.CreateDataReader();
            func.Should().NotThrow();
            func.Invoke().Should().NotBeNull();
        }
    }
}
