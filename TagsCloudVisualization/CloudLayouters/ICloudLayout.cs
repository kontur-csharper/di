using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.CloudLayouters
{
    public interface ICloudLayout
    {
        List<Rectangle> Rectangles { get; }
        Rectangle PutNextRectangle(Size rectangleSize);
    }
}