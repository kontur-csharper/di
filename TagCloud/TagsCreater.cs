using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Layout;
using TagCloud.TextProcessing;

namespace TagCloud
{
    public class TagsCreater: ITagsCreater
    {
        private readonly IFrequencyAnalyzer frequencyAnalyzer;
        private readonly ILayouter layouter;
        private const double heightCoefficient = 8;
        private const double widthCoefficient = 0.7;
        
        public TagsCreater(IFrequencyAnalyzer frequencyAnalyzer, ILayouter layouter)
        {
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.layouter = layouter;
        }
        
        public List<Tuple<string, Rectangle>> GetTags(string filename, int canvasHeight)
        {
            var frequencies = frequencyAnalyzer.GetFrequencyDictionary(filename);
            var tagsCount = frequencies.Count;
            return frequencies
                .OrderByDescending(pair => pair.Value)
                .Select(pair => GetTag(pair, canvasHeight, tagsCount))
                .ToList();
        }

        private Tuple<string, Rectangle> GetTag(KeyValuePair<string, double> pair, int canvasHeight, int tagsCount)
        {
            var frequency = pair.Value;
            var tagString = pair.Key;
            var height = (int) Math.Round(canvasHeight * Math.Sqrt(frequency / heightCoefficient));
            var width = (int)Math.Round((double)height * (tagString.Length) * widthCoefficient);
            var rectangle = layouter.PutNextRectangle(new Size(width, height));
            return new Tuple<string, Rectangle>(tagString, rectangle);
        }
    }
}