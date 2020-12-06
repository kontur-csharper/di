using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class WordTagsLayouter : IWordTagsLayouter
    {
        private readonly IWordsFrequency _wordsFrequency;
        private readonly ICloudLayouter _cloudLayouter;
        private readonly IWordMeasurer _wordMeasurer;
        private readonly Font _font;

        public WordTagsLayouter(IWordsFrequency wordsFrequency, ICloudLayouter cloudLayouter,
            IWordMeasurer wordMeasurer, Font font)
        {
            _wordsFrequency = wordsFrequency;
            _cloudLayouter = cloudLayouter;
            _wordMeasurer = wordMeasurer;
            _font = font;
        }

        public (IReadOnlyList<WordTag>, int) GetWordTagsAndCloudRadius(string text)
        {
            if (text == null)
                throw new Exception("String must be not null");
            var tags = _wordsFrequency
                .GetWordsFrequency(text)
                .OrderByDescending(wordAndFrequency => wordAndFrequency.Value)
                .Select(wordAndFrequency =>
                {
                    var (word, frequency) = wordAndFrequency;
                    var wordFont = new Font(_font.FontFamily,
                        _font.Size + (float) Math.Log2(frequency) * 7);
                    var wordSize = _wordMeasurer.GetWordSize(word, wordFont);
                    var rectangle = _cloudLayouter.PutNextRectangle(wordSize);
                    return new WordTag(word, rectangle, wordFont);
                })
                .ToList();
            return (tags, _cloudLayouter.CloudRadius);
        }
    }
}