﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudContainer;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudVisualization.Tests.TagsCloudContainerTests
{
    public class TagsCloudContainerTests
    {
        private TagsCloudContainer.TagsCloudContainer.TagsCloudContainer container;
        private string newLine = Environment.NewLine;

        [SetUp]
        public void SetUp()
        {
            var parser = new TextParser(new WordValidator());
            var center = new Point(200, 200);
            var layouter = new CircularCloudLayouter(new ArchimedeanSpiral(center, 0.2, 1.0));
            container = new TagsCloudContainer.TagsCloudContainer.TagsCloudContainer(parser, layouter);
        }

        [TestCaseSource(nameof(CountTestCases))]
        public void ShouldContainTags_WhenReturnOnRightText(string text, int expectedCount)
        {
            container.GetTags(text).Count.Should().Be(expectedCount);
        }

        [TestCaseSource(nameof(WordsTestCases))]
        public void ShouldContainTags_WhenReturnOnRightText(string text, List<string> expectedWords)
        {
            container.GetTags(text).Select(x => x.Text).Should().BeEquivalentTo(expectedWords);
        }

        private static IEnumerable<TestCaseData> CountTestCases()
        {
            yield return new TestCaseData("one", 1).SetName("One word");
            yield return new TestCaseData($"one{Environment.NewLine}two{Environment.NewLine}three", 3).SetName(
                "More than one word");
            yield return new TestCaseData($"one{Environment.NewLine}two{Environment.NewLine}two", 2).SetName(
                "Repeating words");
            yield return new TestCaseData($"one{Environment.NewLine}two{Environment.NewLine}TWO", 2).SetName(
                "Different casing");
        }

        private static IEnumerable<TestCaseData> WordsTestCases()
        {
            yield return new TestCaseData("one", new List<string> {"one"}).SetName("One word");
            yield return new TestCaseData($"one{Environment.NewLine}two{Environment.NewLine}three",
                new List<string> {"one", "two", "three"}).SetName("More than one word");
            yield return new TestCaseData($"one{Environment.NewLine}two{Environment.NewLine}two",
                new List<string> {"one", "two"}).SetName("Repeating words");
            yield return new TestCaseData($"one{Environment.NewLine}two{Environment.NewLine}TWO",
                new List<string> {"one", "two"}).SetName("Different casing");
        }
    }
}