using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Common
{
    public class SizeGenerator
    {
        private static readonly Random Random = new Random();
        private readonly int maxHeight;
        private readonly int maxWidth;
        private readonly int minHeight;
        private readonly int minWidth;

        public SizeGenerator(int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            this.minWidth = minWidth;
            this.maxWidth = maxWidth;
            this.minHeight = minHeight;
            this.maxHeight = maxHeight;
        }

        public IEnumerable<Size> GenerateSize(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => new Size(Random.Next(minWidth, maxWidth), Random.Next(minHeight, maxHeight)));
        }
    }
}