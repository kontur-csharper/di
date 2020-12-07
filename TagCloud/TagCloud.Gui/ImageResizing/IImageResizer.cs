using System.Drawing;

namespace TagCloud.Gui.ImageResizing
{
    public interface IImageResizer
    {
        Image Resize(Image source, Size newSize);
    }
}