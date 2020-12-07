using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.App;

namespace TagsCloudTests
{
    [TestFixture]
    internal class BlackListWordsFilterTests
    {
        private readonly WordNormalizer normalizer = new WordNormalizer();

        [TestCase(new[] {"a", "b", "c"}, "a", false)]
        [TestCase(new[] {"a", "b", "c"}, "A", false)]
        [TestCase(new[] {"a", "b", "c"}, "d", true)]
        [TestCase(new[] {"a", "B", "c"}, "b", false)]
        public void BlacklistWordsFilter_ShouldPassWords_ThatAreNotInBlackList(
            IEnumerable<string> blackList, string word, bool expectedResult)
        {
            var filter = new BlackListWordsFilter(blackList, normalizer);
            var wordPassed = filter.Validate(word);
            wordPassed.Should().Be(expectedResult);
        }

        [Test]
        public void BlackListWordsFilter_ShouldThrow_IfCollectionIsNull()
        {
            var filter = new BlackListWordsFilter(new string[0], normalizer);
            Action action = () => filter.Validate(null);
            action.Should().Throw<NullReferenceException>();
        }
    }
}