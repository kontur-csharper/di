using System.Drawing;

namespace TagCloud.Gui.ImageResizing
{
    public class StretchImageResizer : IImageResizer
    {
        public Image Resize(Image source, Size newSize) => new Bitmap(source, newSize);
    }
}