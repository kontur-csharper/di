using NUnit.Framework;
using TagsCloud.TextProcessing.TextFilters;

namespace TagsCloudTest
{
    public class TextFiltersTests
    {
        private FunctionWordsFilter functionWordsFilter;

        [SetUp]
        public void SetUp()
        {
            functionWordsFilter = new FunctionWordsFilter();
        }

        [TestCase("в", ExpectedResult = false, TestName = "Should_Not_Miss_Boring_Words")]
        [TestCase("она", ExpectedResult = false, TestName = "Should_Not_Miss_Boring_Words")]
        [TestCase("утро", ExpectedResult = true, TestName = "Should_Define_Ordinary_Words")]
        public bool FunctionWordsFilter(string word)
        {
            return functionWordsFilter.CanTake(word);
        }
    }
}
