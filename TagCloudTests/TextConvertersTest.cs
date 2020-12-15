using NUnit.Framework;
using System.IO;
using TagCloud.TextConverters.TextProcessors;
using TagCloud.TextConverters.TextReaders;
using TagCloud.WordsMetrics;
using System.Collections.Generic;

namespace TagCloudTests
{
    [TestFixture]
    internal class TextConvertersTest
    {
        [Test]
        public void ReadConvertExcludeAndGetCountMetric()
        {
            var path = $".{Path.DirectorySeparatorChar}fileTest.txt";
            var text = "I\nam\nBetman\nI\nSpeed\nI\nPower\n\nPower\nRanger\nRed\nPower\nRanger";
            File.WriteAllText(path, text);
            var textReader = new TextReaderTxt();
            var textProcessor = new ParagraphTextProcessor();
            var wordsMetric = new CountWordMetric();
            var result = wordsMetric.GetMetric(textProcessor.GetLiterals(textReader.ReadText(path)));
            File.Delete(path);
            var expected = new Dictionary<string, double>();
            expected["am"] = 1;
            expected["betman"] = 1;
            expected["speed"] = 1;
            expected["power"] = 3;
            expected["ranger"] = 2;
            expected["red"] = 1;
            Assert.AreEqual(expected, result);
        }
    }
}
