using System.Drawing;

namespace TagsCloudVisualization.TagCloudLayouter
{
    public interface ICloudLayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize);
        void ClearLayout();
    }
}