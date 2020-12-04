using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.BackgroundPainter
{
    public interface IBackgroundPainter
    {
        void Draw(List<Tuple<string, Rectangle>> tags, ICanvas canvas, Graphics graphics);
    }
}