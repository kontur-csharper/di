using System.Collections.Generic;
using System.Drawing;

namespace WordCloudGenerator
{
    public interface IPainter
    {
        public delegate IPainter Factory(IPalette palette);

        public Bitmap Paint(IEnumerable<GraphicString> words);
    }
}