using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.WordsAnalyzer;
using TagCloud.WordsAnalyzer.WordFilters;
using TagCloud.WordsAnalyzer.WordNormalizer;
using YandexMystem.Wrapper.Enums;

namespace TagCloud_Tests.WordAnalyzer_Tests
{
    [TestFixture]
    public class Analyzer_Tests
    {
        private string text;
        private WordsAnalyzer analyzer;
        
        [SetUp]
        public void SetUp()
        {
            text = "медведь cтоит на середина мост";
                
            var normalizer = new WordNormalizer();
            var boringFilter = new BoringWordFilter(new HashSet<string> {"медведь"});
            var gramPartFilter = new GramPartsFilter(GramPartsEnum.Noun);
            analyzer = new WordsAnalyzer(normalizer, boringFilter, gramPartFilter);
        }
        
        [Test]
        public void AnalyzerResult_ShouldContainAllowedWords()
        {
            var tags = analyzer.GetTags(text.Split());
            tags.Should().Contain(tag => tag.Value == "мост");
            tags.Should().Contain(tag => tag.Value == "середина");
        }
        
        [Test]
        public void AnalyzerResult_ShouldNotContainBoringWords()
        {
            var tags = analyzer.GetTags(text.Split());
            tags.Should().NotContain(tag => tag.Value == "медведь");
        }
        
        [Test]
        public void AnalyzerResult_ShouldNotContainWordsWithWrongGramPart()
        {
            var tags = analyzer.GetTags(text.Split());
            tags.Should().NotContain(tag => tag.Value == "на");
            tags.Should().NotContain(tag => tag.Value == "стоит");
        }
    }
}