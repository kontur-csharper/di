using System.Collections.Generic;

namespace TagCloud
{
    public interface ICloudLayouter
    {
        public List<RectangleWithWord> GetRectangles(IEnumerable<SizeWithWord> sizes);
    }
}