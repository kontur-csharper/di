using System;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.TextProcessing;

namespace TagCloud.Tests
{
    public class FrequencyAnalyzerTest
    {
        private FrequencyAnalyzer frequencyAnalyzer = new FrequencyAnalyzer(
            new TextProcessing.LiteratureTextParser(new PathCreater(), new TxtTextReader()));
        
        [Test]
        public void FrequencyAnalyzerReturnDictionaryWithCorrectFrequencies()
        {
            var dict = frequencyAnalyzer.GetFrequencyDictionary("input.txt");
            var wordsCount = 22;
            dict.Should().HaveCount(12);
            dict["кошка"].Should().BeInRange((double) 6 / wordsCount - Double.Epsilon, (double) 6 / wordsCount + Double.Epsilon);
            dict["собака"].Should().BeInRange((double) 2 / wordsCount - Double.Epsilon, (double) 2 / wordsCount + Double.Epsilon);
            dict["крокодил"].Should().BeInRange((double) 1 / wordsCount - Double.Epsilon, (double) 1 / wordsCount + Double.Epsilon);
        }
    }
}