using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.Canvases
{
    public interface ICanvas
    {
        Size GetImageSize();
        public Point GetImageCenter();
        Graphics StartDrawing();
        void UpdateUi();
        void RecreateImage(ImageSettings settings);
        void SaveImage(string fileName, ImageFormat imageFormat);
    }
}