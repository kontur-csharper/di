using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using HomeExercise;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class FileProcessorTests
    {
        [Test]
        public void GetWord_ReturnCorrectWord_WhenFileContainsOneWord()
        {
            var finalPath = GetWordsPath("oneWord.txt"); ;
            var fileProcessor = new FileProcessor(finalPath, null);
            using (StreamWriter sw = new StreamWriter(finalPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLineAsync("word");
            }
            
            var resultWords = fileProcessor.GetWords();
            resultWords.First().Key.Should().BeEquivalentTo("word");
        }
        
        [Test]
        public void GetWord_ReturnCorrectWords_WhenFileContainsOneWordManyTimes()
        {
            var finalPath = GetWordsPath("ManyWords.txt");
            var fileProcessor = new FileProcessor(finalPath, null);
            using (StreamWriter sw = new StreamWriter(finalPath, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < 100; i++)
                {
                    sw.WriteLineAsync("word");
                }
            }
            
            var resultWords = fileProcessor.GetWords();
            var expected = new Dictionary<string, int> {{"word", 100}};
            resultWords.Should().BeEquivalentTo(expected);
        }
        
        [Test]
        public void GetWord_ReturnCorrectWords_WhenFileOneWordWithDifferentRegisters()
        {
            var finalPath = GetWordsPath("TwoWords.txt");
            var fileProcessor = new FileProcessor(finalPath, null);
            using (var sw = new StreamWriter(finalPath, false, System.Text.Encoding.Default))
            {
                sw.WriteLineAsync("WoRd");
                sw.WriteLineAsync("word");
            }
            
            var resultWords = fileProcessor.GetWords();
            var expected = new Dictionary<string, int> {{"word", 2}};
            resultWords.Should().BeEquivalentTo(expected);
        }

        private string GetWordsPath(string path)
        {
            var dirPath = AppDomain.CurrentDomain.BaseDirectory;

            return Path.Combine(dirPath, path);
        }
    }
}