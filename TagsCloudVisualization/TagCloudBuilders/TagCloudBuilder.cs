using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.FontHandlers;
using TagsCloudVisualization.StringExtensions;
using TagsCloudVisualization.TagCloudLayouter;
using TagsCloudVisualization.Tags;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualization.TagCloudBuilders
{
    public class TagCloudBuilder : ITagCloudBuilder
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly FontSettings fontSettings;

        public TagCloudBuilder(ICloudLayouter cloudLayouter, FontSettings fontSettings)
        {
            this.cloudLayouter = cloudLayouter;
            this.fontSettings = fontSettings;
        }

        public IReadOnlyList<Tag> Build(IEnumerable<Word> wordsFrequency)
        {
            cloudLayouter.ClearLayout();

            return (from word in wordsFrequency
                let font = FontHandler.CalculateFont(word.Weight, fontSettings)
                let tagSize = word.Value.MeasureString(font)
                let rectangle = cloudLayouter.PutNextRectangle(tagSize)
                select new Tag(word.Value, rectangle, font)).ToList();
        }
    }
}