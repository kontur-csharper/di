using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.BoringWordsDetectors;
using TagsCloud.StatisticProviders;

namespace TagsCloudTests
{
    [TestFixture]
    public class StatisticProvider_Should
    {
        private readonly StatisticProvider provider = new StatisticProvider(new ByCollectionBoringWordsDetector());
        
        [Test]
        public void ConvertWords_ToLowerCase()
        {
            var words = new[] {"ABC", "1a2B3c", "аБвГ"};

            provider.GetWordStatistics(words).Keys.Should().BeEquivalentTo(words.Select(w => w.ToLower()));
        }

        [Test]
        public void Count_AllNonBoringWords()
        {
            var words = new[] {"ABC", "abc", "aBc", "cde", "cde", "123"};
            var actual = provider.GetWordStatistics(words);

            actual.Should().ContainKeys("abc", "cde", "123");
            actual["abc"].Should().Be(3);
            actual["cde"].Should().Be(2);
            actual["123"].Should().Be(1);
        }
    }
}