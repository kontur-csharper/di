using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.ProgramOptions;
using TagsCloudContainer.WordsParser;

namespace TagsCloudContainerTests
{
    public class WordsAnalyzerTest
    {
        private IWordsAnalyzer wordsAnalyzer;
        private WordReaderTest wordReader;
        private IFilterOptions options;
        
        [SetUp]
        public void SetUp()
        {
            options = new OptionsStub(0, 0);
            wordReader = new WordReaderTest();
            wordsAnalyzer = new WordsAnalyzer(new Filter(options), wordReader);
        }

        [Test]
        public void AnalyzeWordsShouldReturnEmpty_WhenNoWordsWereFound()
        {
            wordsAnalyzer.AnalyzeWords().Count.Should().Be(0);
        }
        
        [Test]
        public void AnalyzedWordsShouldBeLowercase()
        {
            wordReader.AddWords(new []{"FirsT", "first"});
            var words = wordsAnalyzer.AnalyzeWords();
            
            words.Count.Should().Be(1);
        }

        [Test]
        public void AnalyzeWordsShouldCountCorrect()
        {
            wordReader.AddWords(new []{"first", "second", "third", "third", "second", "third"});
            var words = wordsAnalyzer.AnalyzeWords();

            words.Count.Should().Be(3);
            words["first"].Should().Be(1);
            words["second"].Should().Be(2);
            words["third"].Should().Be(3);
        }
        
        [Test]
        public void AnalyzeWordsShouldIgnoreBoringWords()
        {
            options.BoringWords = new[] {"first"};
            wordsAnalyzer = new WordsAnalyzer(new Filter(options), wordReader);
            wordReader.AddWords(new []{"first", "second"});
            var words = wordsAnalyzer.AnalyzeWords();

            words.Count.Should().Be(1);
        }
    }

    public class WordReaderTest: IWordReader
    {
        private readonly Stack<string> words;
        
        public WordReaderTest()
        {
            words = new Stack<string>();
        }

        public void AddWords(string[] inputWords)
        {
            foreach (var word in inputWords)
                words.Push(word);
        }
        
        public string ReadWord()
        {
            return words.Count == 0 ? null : words.Pop();
        }
    }
}