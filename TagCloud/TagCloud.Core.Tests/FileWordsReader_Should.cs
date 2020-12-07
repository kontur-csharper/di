using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Text;

namespace TagCloud.Core.Tests
{
    // ReSharper disable once InconsistentNaming
    public class FileWordsReader_Should
    {
        private static readonly string allowedWordChars = new string(GetCharsByCodeRange('a', 'z')
            .Concat(GetCharsByCodeRange('A', 'Z'))
            .Concat(GetCharsByCodeRange('0', '9'))
            .ToArray());

        private static readonly TestCaseData[] validSeparatorsTestCases =
            new[] {",", ".", "!", "(", ")", "[", "]", "{", "}", "-", "=", "/", "\\", ";", ":"}
                .Select(s => new TestCaseData(s) {TestName = s})
                .Concat(new[]
                {
                    new TestCaseData(" ") {TestName = "whitespace"},
                    new TestCaseData("\r\n") {TestName = @"\r\n"},
                    new TestCaseData("\n") {TestName = @"\n"},
                })
                .ToArray();

        private FileWordsReader reader;
        private string filePath;

        [SetUp]
        public void SetUp()
        {
            reader = new FileWordsReader();
            filePath = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                TestContext.CurrentContext.Test.MethodName + ".txt");
        }

        [TearDown]
        public void TearDown()
        {
            if (!string.IsNullOrEmpty(filePath))
                File.Delete(filePath);
        }

        [Test]
        public void FileMissing_ShouldThrow()
        {
            Action test = () => reader.GetWordsFrom(filePath);
            test.Should()
                .Throw<FileNotFoundException>();
        }

        [Test]
        public void FileExists_ShouldNotThrow()
        {
            CreateTestFile(GetRandomWord());
            Action test = () => reader.GetWordsFrom(filePath);
            test.Should()
                .NotThrow();
        }

        [TestCaseSource(nameof(validSeparatorsTestCases))]
        public void SplitToWords_WithSeparator(string separator)
        {
            var words = GetRandomWords(2);
            CreateTestFile(string.Join(separator, words));

            reader.GetWordsFrom(filePath)
                .Should()
                .BeEquivalentTo(words);
        }

        private void CreateTestFile(string content) => File.WriteAllText(filePath, content);

        private string[] GetRandomWords(int count) => Enumerable.Repeat(0, count)
            .Select(_ => GetRandomWord())
            .ToArray();

        private string GetRandomWord() => TestContext.CurrentContext.Random.GetString(5, allowedWordChars);

        private static IEnumerable<char> GetCharsByCodeRange(char from, char to)
        {
            for (var i = (int) from; i <= (int) to; i++)
                yield return (char) i;
        }
    }
}