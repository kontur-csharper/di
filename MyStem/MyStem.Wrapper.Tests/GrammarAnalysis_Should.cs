using System.IO;
using FluentAssertions;
using MyStem.Wrapper.Workers.Grammar;
using MyStem.Wrapper.Workers.Grammar.Raw;
using MyStem.Wrapper.Wrapper;
using NUnit.Framework;

namespace MyStem.Wrapper.Tests
{
    // ReSharper disable once InconsistentNaming
    public class GrammarAnalysis_Should
    {
        private IGrammarAnalyser analyser;

        [SetUp]
        public void SetUp()
        {
            analyser = new GrammarAnalyser(new MyStemBuilder(Path.Combine(TestContext.CurrentContext.WorkDirectory,
                "../../../../bin/", "mystem.exe")));
        }

        [Test]
        public void CreateRawResultModel_ForEachWord()
        {
            PerformTest("барашки должны плодиться")
                .Should()
                .HaveCount(3);
        }

        [Test]
        public void CreateRawResultEntry_ForEachEntryInWord()
        {
            PerformTest("барашка")
                .Should()
                .ContainSingle()
                .Which.Entries
                .Should()
                .HaveCount(2);
        }

        [Test]
        public void EnglishWord_ReturnResultWithoutEntries()
        {
            PerformTest("abc")
                .Should()
                .BeEquivalentTo(new AnalysisResultRaw
                {
                    Text = "abc",
                    Entries = new AnalysisResultEntryRaw[0]
                });
        }

        [Test]
        public void DigitsOnly_ReturnNothing()
        {
            PerformTest("1234")
                .Should()
                .BeEmpty();
        }

        [Test]
        public void NonDictWord_ExtractQuality()
        {
            PerformTest("баребуха")
                .Should()
                .ContainSingle()
                .Which
                .Entries
                .Should()
                .ContainSingle()
                .Which
                .Quality
                .Should()
                .NotBeNullOrWhiteSpace();
        }

        private AnalysisResultRaw[] PerformTest(string input)
        {
            var result = analyser.GetRawResult(input);
            foreach (var entry in result)
                TestContext.Progress.WriteLine(entry.ToString());
            return result;
        }
    }
}