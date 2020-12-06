using FluentAssertions;
using NUnit.Framework;
using WordCloudGenerator;

namespace Tests
{
    public class PreparerTests
    {
        [Test]
        public void CreateWordFreqList_ShouldReturnFrequencies_OneWordInEachLine()
        {
            var text = "abc\nabc\nefg";
            var preparer = new Preparer(null);

            preparer.CreateWordFreqList(text).Should().BeEquivalentTo(
                new[] {new WordFrequency("abc", 2f / 3), new WordFrequency("efg", 1f / 3)});
        }

        [Test]
        public void CreateWordFreqList_ShouldSkipBoringWords_OneBoringWord()
        {
            var boringWords = new[] {"abc"};
            var text = "abc\nabc\nefg\nefg\nxyz";
            var preparer = new Preparer(boringWords);

            preparer.CreateWordFreqList(text).Should().BeEquivalentTo(
                new[] {new WordFrequency("efg", 2f / 3), new WordFrequency("xyz", 1f / 3)});
        }

        [Test]
        public void CreateWordFreqList_ShouldSplitWords_PlainText()
        {
            var text = "abc abc efg\nefg xyz";
            var preparer = new Preparer(new []{""});
            preparer.CreateWordFreqList(text).Should().BeEquivalentTo(
                new[] {new WordFrequency("abc", 2f / 5), new WordFrequency("efg", 2f / 5), new WordFrequency("xyz", 1f / 5)});
        }

        [Test]
        public void CreateWordFreqList_ShouldSkipBoringWords_PlainText()
        {
            var boringWords = new[] {"abc"};
            var text = "abc abc efg\nefg xyz";
            var preparer = new Preparer(boringWords);

            preparer.CreateWordFreqList(text).Should().BeEquivalentTo(
                new[] {new WordFrequency("efg", 2f / 3), new WordFrequency("xyz", 1f / 3)});
        }
    }
}