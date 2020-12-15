using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudVisualization.App
{
    public sealed class TagsCloudPictureHolder : PictureBox
    {
        private ImageSettings ImageSettings { get; }

        public TagsCloudPictureHolder(ImageSettings imageSettings)
        {
            ImageSettings = imageSettings;
            Dock = DockStyle.Fill;
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = new Bitmap(imageSettings.Width, imageSettings.Height, PixelFormat.Format24bppRgb);
            BackColor = imageSettings.BackColor;
        }

        public void SaveImage(string fileName) => Image.Save(fileName);

        public void RecreateImage()
        {
            Image = new Bitmap(ImageSettings.Width, ImageSettings.Height, PixelFormat.Format24bppRgb);
            BackColor = ImageSettings.BackColor;
        }

        public void UpdateUi()
        {
            Refresh();
            Application.DoEvents();
        }
    }
}