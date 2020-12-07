using System.Drawing;

namespace TagsCloud.LayoutAlgorithms
{
    public interface ILayoutAlgorithm
    {
        public Rectangle PutNextRectangle(Size size);
        public Size GetSize();
    }
}
