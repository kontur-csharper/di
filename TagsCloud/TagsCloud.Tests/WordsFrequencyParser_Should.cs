using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.WordsProcessing;

namespace TagsCloud.Tests
{
    class WordsFrequencyParser_Should
    {
        private List<string> words = new List<string>();
        private string testFilePath = Assembly.GetExecutingAssembly().Location + "test.txt";

        [SetUp]
        public void SetUp()
        {
            var words = new List<string> {"repeat", "repeat", "repeat", "repeat", "repeat", "one", "two", "three", "four", "five"};
            this.words = words;
            var sampleText = string.Join(Environment.NewLine, words);
            File.AppendAllText(testFilePath, sampleText);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(testFilePath);
        }

        [TestCase("")]
        [TestCase("repeat")]
        public void ProvideCorrectData(params string[] toIgnore)
        {
            var filter = new WordsFilter(toIgnore.ToHashSet());
            var parser = new WordsFrequencyParser(filter);
            parser.ParseWordsFrequencyFromFile(testFilePath)
                .Should().NotContainKeys(toIgnore)
                .And.ContainKeys(words.Where(word => !toIgnore.Contains(word)));
        }

        [TestCase("not single word in one string")]
        [TestCase(":3")]
        public void Throw_WhenIncorrectFileFormat(string wrongFormattedString)
        {
            File.AppendAllText(testFilePath, wrongFormattedString);
            var parser = new WordsFrequencyParser(new WordsFilter(new HashSet<string>()));
            Assert.Throws<FormatException>(() => parser.ParseWordsFrequencyFromFile(testFilePath));
        }
    }
}
