using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.Canvases
{
    public sealed class Canvas : PictureBox, ICanvas
    {
        public Canvas(ImageSettings imageSettings)
        {
            RecreateImage(imageSettings);
            Dock = DockStyle.Fill;
        }

        public Size GetImageSize()
        {
            return Image.Size;
        }

        public Point GetImageCenter()
        {
            var imageSize = Image.Size;
            return new Point(imageSize.Width / 2, imageSize.Height / 2);
        }
        
        public Graphics StartDrawing()
        {
            return Graphics.FromImage(Image);
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }

        public void RecreateImage(ImageSettings imageSettings)
        {
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage(string fileName, ImageFormat imageFormat)
        {
            Image.Save(fileName, imageFormat);
        }
    }
}