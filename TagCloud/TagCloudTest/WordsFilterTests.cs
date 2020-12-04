using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NHunspell;
using NUnit.Framework;
using TagCloud.WordsFilter;

namespace TagCloudTest
{
    [TestFixture]
    public class WordsFilterTests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var dictionaryAff = Path.GetFullPath("../../../dictionaries/en.aff");
            var dictionaryDic = Path.GetFullPath("../../../dictionaries/en.dic");
            WordsFilter = new WordsFilter(new Hunspell(dictionaryAff, dictionaryDic)).RemovePrepositions().Normalize();
        }

        private IWordsFilter WordsFilter;

        [Test]
        public void Prepositions_ShouldBeRemoved()
        {
            WordsFilter.Apply(new List<string> {"123", "in", "of", "word", "anotherword"})
                .Should().BeEquivalentTo(new List<string> {"123", "word", "anotherword"});
        }

        [Test]
        public void Words_ShouldBeNormalized()
        {
            WordsFilter.Apply(new List<string> {"123", "in", "of", "contains", "words", "anoTHerWord"})
                .Should().BeEquivalentTo(new List<string> {"123", "contain", "word", "anotherword"});
        }
    }
}