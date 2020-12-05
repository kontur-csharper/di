using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudContainer;

namespace TagsCloudVisualization.Tests.TagsCloudContainerTests
{
    public class TextParserTests
    {
        private TextParser Parser { get; set; }

        [SetUp]
        public void SetUp()
        {
            Parser = new TextParser(new WordValidator());
        }

        [TestCaseSource(nameof(TestCases))]
        public void ShouldReturnExpectedResult_When(string text, List<string> expectedResult)
        {
            Parser.GetAllWords(text).Should().BeEquivalentTo(expectedResult);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData("one two three", new List<string> {"one", "two", "three"}).SetName(
                "Just words");
            yield return new TestCaseData($"new. {Environment.NewLine} line.", new List<string> {"new", "line"})
                .SetName("New line");
            yield return new TestCaseData("TEXT", new List<string> {"text"}).SetName("Upper case");
            yield return new TestCaseData("Text", new List<string> {"text"}).SetName("Capitalised");
            yield return new TestCaseData("i am a boring", new List<string> {"boring"}).SetName("Boring words");
            yield return new TestCaseData("one one two", new List<string> {"one", "one", "two"}).SetName(
                "More than one entry");
        }
    }
}