using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Interfaces;
using TagsCloudVisualization;

namespace TagCloud
{
    public class WordsForCloudGenerator : IWordsForCloudGenerator
    {
        private readonly string fontName;
        private readonly int maxFontSize;
        private readonly ITagCloudLayouter tagCloudLayouter;
        private readonly IColorGenerator colorGenerator;

        public WordsForCloudGenerator(string fontName, int maxFontSize, ITagCloudLayouter tagCloudLayouter,
                                      IColorGenerator colorGenerator)
        {
            this.tagCloudLayouter = tagCloudLayouter;
            this.fontName = fontName;
            this.maxFontSize = maxFontSize;
            this.colorGenerator = colorGenerator;
        }

        public List<WordForCloud> Generate(List<string> words)
        {
            var wordFrequency = GetWordsFrequency(words)
                                .OrderBy(x => x.Value)
                                .Reverse()
                                .ToList();

            var maxFrequency = wordFrequency.FirstOrDefault().Value;
            return wordFrequency
                   .Select(x =>
                               GetWordForCloud(fontName,
                                               maxFontSize,
                                               colorGenerator.GetNextColor(),
                                               x.Key,
                                               x.Value,
                                               maxFrequency))
                   .ToList();
        }

        private static Dictionary<string, int> GetWordsFrequency(List<string> words) =>
            words.GroupBy(x => x)
                 .ToDictionary(group => group.Key,
                               group => group.Count());

        private WordForCloud GetWordForCloud(string font, int maxWordSize, Color color, string word,
                                             int wordFrequency, int maxFrequency)
        {
            var wordFontSize = (int) Math.Round(maxWordSize * (double) wordFrequency / maxFrequency);
            var wordSize = new Size((int) (word.Length * (wordFontSize * 0.8)), wordFontSize + 12);

            return new WordForCloud(font, wordFontSize, word, tagCloudLayouter.PutNextRectangle(wordSize), color);
        }
    }
}