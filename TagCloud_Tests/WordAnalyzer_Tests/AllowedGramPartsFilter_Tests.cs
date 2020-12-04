using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using TagCloud.WordsAnalyzer.WordFilters;
using YandexMystem.Wrapper.Enums;

namespace TagCloud_Tests.WordAnalyzer_Tests
{
    [TestFixture]
    public class AllowedGramPartsFilter_Tests
    {
        private GramPartsFilter nounFilter;
        private GramPartsFilter verbFilter;
        private GramPartsFilter nounAndVerbFilter;
        
        [SetUp]
        public void SetUp()
        {
            nounFilter = new GramPartsFilter(GramPartsEnum.Noun);
            verbFilter = new GramPartsFilter(GramPartsEnum.Verb);
            nounAndVerbFilter = new GramPartsFilter(GramPartsEnum.Noun, GramPartsEnum.Verb);
        }
        
        [TestCase("торт")]
        [TestCase("сахар")]
        [TestCase("сливки")]
        public void NounFilter_ShouldNotExclude_WhenNounWord(string word)
        {
            nounFilter.ShouldExclude(word).Should().BeFalse();
        }
        
        [TestCase("готовить")]
        [TestCase("убираться")]
        [TestCase("приехал")]
        public void VerbFilter_ShouldNotExclude_WhenVerbWord(string word)
        {
            verbFilter.ShouldExclude(word).Should().BeFalse();
        }
        
        [TestCase("торт")]
        [TestCase("сахар")]
        [TestCase("сливки")]
        [TestCase("готовить")]
        [TestCase("убираться")]
        [TestCase("приехал")]
        public void NounVerbFilter_ShouldNotExclude_WhenNounOrVerbWord(string word)
        {
            nounAndVerbFilter.ShouldExclude(word).Should().BeFalse();
        }
        
        [TestCase("красивый")]
        [TestCase("много")]
        public void Filter_ShouldExclude_WhenNotNounOrVerbWord(string word)
        {
            nounAndVerbFilter.ShouldExclude(word).Should().BeTrue();
        }
        
        [TestCase("вы")]
        [TestCase("я")]
        [TestCase("под")]
        [TestCase("с")]
        [TestCase("мною")]
        public void Filter_ShouldExclude_WhenBoringGramPart(string word)
        {
            nounAndVerbFilter.ShouldExclude(word).Should().BeTrue();
        }
    }
}