using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NHunspell;
using NUnit.Framework;
using TagsCloud.Core;

namespace TagsCloud.Tests
{
    internal class TextAnalyzer_Tests
    {
        private static readonly string PathToDictionary = "../../../Texts/ru_RU.dic";
        private static readonly string PathToAffix = "../../../Texts/ru_RU.aff";
        private static readonly Hunspell Hunspell = new Hunspell(PathToAffix, PathToDictionary);

        [Test]
        public void GetWordByFrequency_CorrectFrequencyWithoutSorting()
        {
            var text = "русское\nрусский\nрусского\nбани\nконь\nконя";
            var expected = new List<(string, int)>
            {
                ("русский", 3),
                ("баня", 1),
                ("конь", 2)
            };

            var actual = TextAnalyzer.GetWordByFrequency(text.Split('\n'), new HashSet<string>(), Hunspell, x => x);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordByFrequency_CorrectFrequencyWithSorting()
        {
            var text = "баян\nрусское\nрусский\nрусского\nбани\nконь\nконя\nбанный";
            var expected = new List<(string, int)>
            {
                ("русский", 3),
                ("конь", 2),
                ("банный", 1),
                ("баня", 1),
                ("баян", 1)
            };

            var actual = TextAnalyzer.GetWordByFrequency(
                text.Split('\n'),
                new HashSet<string>(),
                Hunspell,
                x => x.OrderByDescending(y => y.Value)
                    .ThenByDescending(y => y.Key.Length)
                    .ThenBy(y => y.Key, StringComparer.Ordinal));

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordByFrequency_CorrectFrequencyWithoutBoringWords()
        {
            var text = "баян\nи\nа\nно\nбаян\nда\nя";
            var expected = new List<(string, int)>
            {
                ("баян", 2)
            };

            var actual = TextAnalyzer.GetWordByFrequency(
                text.Split('\n'),
                new HashSet<string> {"и", "а", "но", "да", "я"},
                Hunspell,
                x => x.OrderByDescending(y => y.Value)
                    .ThenByDescending(y => y.Key.Length)
                    .ThenBy(y => y.Key, StringComparer.Ordinal));

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetWordByFrequency_CorrectFrequencyAllWordsInLowerCase()
        {
            var text = "РУССКОЕ\nРусский\nРуССкогО\nбани\nКОНЬ\nконя";
            var expected = new List<(string, int)>
            {
                ("русский", 3),
                ("баня", 1),
                ("конь", 2)
            };

            var actual = TextAnalyzer.GetWordByFrequency(text.Split('\n'), new HashSet<string>(), Hunspell, x => x);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}