using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.TagsCloudContainer.Interfaces;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudContainer
{
    public class TagsCloudContainer : ITagsContainer
    {
        private readonly ILayouterFactory factory;
        private readonly ITextParser parser;

        public TagsCloudContainer(ITextParser parser, ILayouterFactory factory)
        {
            this.parser = parser;
            this.factory = factory;
        }

        public List<Tag> GetTags(string text, SpiralType type)
        {
            var wordEntry = GetWordEntry(text);
            var tags = new List<Tag>();
            var graphics = Graphics.FromImage(new Bitmap(1, 1));
            var layouter = factory.GetLayouter(type);

            foreach (var word in wordEntry.Keys.ToList())
            {
                var wordFont = new Font("Arial", wordEntry[word] + 10);
                var wordSize = graphics.MeasureString(word, wordFont).ToSize();
                var rectangle = layouter.PutNextRectangle(wordSize);

                tags.Add(new Tag(word, rectangle, wordFont, Brushes.Black));
            }

            return tags;
        }

        private Dictionary<string, int> GetWordEntry(string text)
        {
            return parser
                .GetAllWords(text)
                .GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
        }
    }
}