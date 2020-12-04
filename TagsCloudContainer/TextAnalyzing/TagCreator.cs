using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Common;

namespace TagsCloudContainer.TextAnalyzing
{
    public class TagCreator
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly FontSettings fontSettings;
        private readonly IImageHolder imageHolder;
        private readonly TextAnalyzer textAnalyzer;

        public TagCreator(FontSettings fontSettings, ICloudLayouter cloudLayouter, TextAnalyzer textAnalyzer,
            IImageHolder imageHolder)
        {
            this.fontSettings = fontSettings;
            this.cloudLayouter = cloudLayouter;
            this.textAnalyzer = textAnalyzer;
            this.imageHolder = imageHolder;
        }

        public IEnumerable<Tag> GetTagsForVisualization()
        {
            using (cloudLayouter)
            {
                var wordsWithFrequency = textAnalyzer.GetWordWithFrequency();
                var minFrequency = wordsWithFrequency.Min(wordWithFrequency => wordWithFrequency.Value);
                var maxFrequency = wordsWithFrequency.Max(wordWithFrequency => wordWithFrequency.Value);
                foreach (var wordWithFrequency in wordsWithFrequency)
                {
                    var fontSize = GetFontSizeForWord(minFrequency, maxFrequency, wordWithFrequency.Value,
                        wordWithFrequency.Key);
                    if (fontSize < fontSettings.MinFontSize)
                        continue;
                    var font = new Font("Times New Roman", fontSize);
                    using (var graphics = imageHolder.StartDrawing())
                    {
                        var textSizeF = graphics.MeasureString(wordWithFrequency.Key, font);
                        var rectangleSize = new Size((int) Math.Ceiling(textSizeF.Width),
                            (int) Math.Ceiling(textSizeF.Height));
                        yield return new Tag(cloudLayouter.PutNextRectangle(rectangleSize), wordWithFrequency.Key,
                            font);
                    }
                }
            }
        }

        public int GetFontSizeForWord(int minFrequency, int maxFrequency, int wordFrequency, string word)
        {
            var fontSize = wordFrequency > minFrequency
                ? fontSettings.MaxFontSize * (wordFrequency - minFrequency) / (maxFrequency - minFrequency)
                : 1;
            if (fontSize == 0)
                fontSize = 1;
            return fontSize;
        }
    }
}