using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public interface IImageColorProvider
    {
        Color GetColor();
        void AddColors(IEnumerable<Color> colors);
    }
}