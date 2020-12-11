using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Tests
{
    public class WordFrequency_Should
    {
        private string[] testData;
        private IWordChecker wordChecker;
        private WordFrequency wordFrequency;

        [SetUp]
        public void SetUp()
        {
            testData = new[] {"ASD", "aSd", "asd", "zxc", "zxc", "qwe"};
            wordChecker = A.Fake<IWordChecker>();
            wordFrequency = new WordFrequency(wordChecker);
        }

        [Test]
        public void OnGet_CallWordCheckerOnEveryElement()
        {
            wordFrequency.Get(testData);
            A.CallTo(() => wordChecker.IsWordNotBoring(A<string>.Ignored))
                .MustHaveHappenedANumberOfTimesMatching(x => x == testData.Length);
        }

        [Test]
        public void Get_ReturnsCorrectFrequency()
        {
            A.CallTo(() => wordChecker.IsWordNotBoring(A<string>.Ignored)).Returns(true);
            var expected = new Dictionary<string, double>
            {
                ["asd"] = Math.Round(3.0 / 6, 2),
                ["zxc"] = Math.Round(2.0 / 6, 2),
                ["qwe"] = Math.Round(1.0 / 6, 2)
            };
            wordFrequency.Get(testData).Should().BeEquivalentTo(expected);
        }
    }
}