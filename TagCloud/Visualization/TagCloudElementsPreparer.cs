﻿using System.Collections.Generic;
using System.Linq;
using TagCloud.Algorithm;
using TagCloud.App;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public class TagCloudElementsPreparer : ITagCloudElementsPreparer
    {
        private readonly ITagCloudLayouter tagCloudLayouter;
        private readonly IWordPainterProvider wordPainterProvider;
        private readonly PictureConfig config;

        public TagCloudElementsPreparer(
            ITagCloudLayouter tagCloudLayouter, 
            IWordPainterProvider wordPainterProvider, 
            PictureConfig config)
        {
            this.tagCloudLayouter = tagCloudLayouter;
            this.wordPainterProvider = wordPainterProvider;
            this.config = config;
        }

        public IEnumerable<TagCloudElement> PrepareTagCloudElements(IEnumerable<Word> words)
        {
            var wordPainter = wordPainterProvider.GetWordPainter();
            foreach (var pair in words.Select((w, i) => new { Word = w, Index = i }))
            {
                var word = pair.Word;
                var index = pair.Index;
                var rectangle = tagCloudLayouter.PutNextRectangle(word.WordRectangleSize);
                var color = wordPainter.GetWordColor(word, index);
                yield return new TagCloudElement(word, rectangle, color, config.FontFamily);
            }
        }
    }
}