using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter : IWordsLayouter
    {
        public IEnumerable<LayoutedWord> LayoutWords(IEnumerable<LayoutedWord> words)
        {
            var layouter = new TagsCloudVisualization.CircularCloudLayouter(Point.Empty);
            return words
                .OrderByDescending(word => word.Count)
                .Select(w => new LayoutedWord(w.Word, w.Count,
                    layouter.PutNextRectangle(ToSize(w.Rectangle.Size))));
        }

        private Size ToSize(SizeF size) => new Size((int) Math.Ceiling(size.Width), (int) Math.Ceiling(size.Height));
    }
}