using System.Collections.Generic;
using System.Drawing;
using TagCloud.Core;

namespace TagCloudUI.Infrastructure
{
    public class LayoutInfo
    {
        public List<Tag> Tags { get; }
        public Size Size { get; }

        public LayoutInfo(List<Tag> tags, Size size)
        {
            Tags = tags;
            Size = size;
        }
    }
}