using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TextWriter = TagsCloudContainer.TagsCloudContainer.TextWriter;

namespace TagsCloudVisualization.Tests.TagsCloudContainerTests
{
    public class TextWriterTests
    {
        private TextWriter Writer { get; set; }
        private string Root { get; set; }

        [SetUp]
        public void SetUp()
        {
            Writer = new TextWriter();
            Root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
        }

        [TestCaseSource(nameof(PathTestCases))]
        public void ThrowException_When(string path)
        {
            Action writeText = () => Writer.WriteText("text", path);

            writeText.Should().Throw<ArgumentException>();
        }

        [Test]
        public void DoesntThrowException_When()
        {
            var path = $"..{Path.DirectorySeparatorChar}image.png";
            Action writeText = () => Writer.WriteText("text", path);

            writeText.Should().NotThrow<ArgumentException>();
            File.Delete(path);
        }

        [TestCaseSource(nameof(TestCases))]
        public void SaveFileInRightFormat_When(string text, string expectedResult)
        {
            var path = Path.Join(Root, "TagsCloudContainer", "Texts", "test.txt");
            Writer.WriteText(text, path);

            File.Exists(path).Should().BeTrue();
            File.ReadAllText(path).Should().Be(expectedResult);
            File.Delete(path);
        }

        private static IEnumerable<TestCaseData> PathTestCases()
        {
            yield return new TestCaseData("<html></html>").SetName("Directory dont exist");
            yield return new TestCaseData(".. Dir text.txt").SetName("Not platform separator");
            yield return new TestCaseData($@"..{Path.DirectorySeparatorChar}text txt").SetName(
                "Doesnt have dot separator");
            yield return new TestCaseData($@"..{Path.DirectorySeparatorChar}text.").SetName(
                "Doesnt have filename extension");
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData("one two three", $"one{Environment.NewLine}two{Environment.NewLine}three")
                .SetName("Just words");
            yield return new TestCaseData("", "").SetName("Empty string");
            yield return new TestCaseData("Dot, net.", $"Dot{Environment.NewLine}net").SetName("With delimiters");
            yield return new TestCaseData("12 34", $"12{Environment.NewLine}34").SetName("Digits");
            yield return new TestCaseData($"New. {Environment.NewLine} line.", $"New{Environment.NewLine}line")
                .SetName("New line");
        }
    }
}