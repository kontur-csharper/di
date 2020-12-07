using System;
using System.IO;
using FluentAssertions;
using MyStem.Wrapper.Enums;
using MyStem.Wrapper.Wrapper;
using NUnit.Framework;

namespace MyStem.Wrapper.Tests
{
    // ReSharper disable once InconsistentNaming
    public class MyStemProcess_Should
    {
        private string path;
        private IMyStem process;

        [SetUp]
        public void Setup()
        {
            path = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                $"{Guid.NewGuid()}.exe"
            );

            File.Copy(
                Path.Combine(TestContext.CurrentContext.WorkDirectory, "../../../../bin/", "mystem.exe"),
                path,
                overwrite: true
            );

            process = new MyStemBuilder(path).Create(MyStemOutputFormat.Text);
        }

        [Test]
        public void FileMissing_ShouldThrow()
        {
            File.Delete(path);

            Action test = () => process.GetResponse("бибочка");
            test.Should()
                .Throw<FileNotFoundException>();
        }

        [Test]
        public void FileExists_ShouldNotThrow()
        {
            Action test = () => process.GetResponse("бибочка");
            test.Should()
                .NotThrow();
        }

        [Test]
        public void ReturnCorrectResponse()
        {
            process.GetResponse("упячки")
                .Should()
                .Be("упячки{упячка?}");
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(path);
        }
    }
}