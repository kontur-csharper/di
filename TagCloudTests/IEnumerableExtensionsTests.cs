using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Extensions;

namespace TagCloudTests
{
    class IEnumerableExtensionsTests
    {
        [TestCase("a", TestName = "OneElement")]
        [TestCase("a", "a", "a", TestName = "OneDifferentItem")]
        [TestCase("a", "a", "b", TestName = "ElementsHaveDifferentFrequencies")]
        [TestCase("a", "b", "c", TestName = "AllElementsHaveSameFrequency")]
        public void MostFrequent_ShouldReturnElementsInDescendingOrder(params string[] words)
        {
            words.MostFrequent().Should().BeInDescendingOrder(el => el.frequency);
        }

        [TestCase(new[] {"a"}, new[] {1}, TestName = "OneElement")]
        [TestCase(new[] {"a", "a", "a"}, new[] {3}, TestName = "OneDifferentItem")]
        [TestCase(new[] {"a", "a", "b"}, new[] {2, 1}, TestName = "ElementsHaveDifferentFrequencies")]
        [TestCase(new[] {"a", "b", "c"}, new[] {1, 1, 1}, TestName = "AllElementsHaveSameFrequency")]
        public void MostFrequent_ShouldReturnElementsWithTheirFrequencies(
            string[] words, int[] expectedFrequencies)
        {
            words.MostFrequent()
                .Select(el => el.frequency).Should().BeEquivalentTo(expectedFrequencies);
        }

        [TestCase("a", "b", "c", "a", "b", "c", "a", "b", "c")]
        public void MostFrequent_ShouldReturnOnlyDifferentElements(params string[] words)
        {
            var mostFrequent = words.MostFrequent().ToList();

            mostFrequent
                .Select((item, index) => mostFrequent.Skip(index + 1).Count(item.Equals))
                .Sum().Should().Be(0);
        }
    }
}