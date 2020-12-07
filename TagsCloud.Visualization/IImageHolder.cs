using System.Drawing;

namespace TagsCloud.Visualization
{
    public interface IImageHolder
    {
        Size GetImageSize();
        Graphics StartDrawing();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName);
    }
}