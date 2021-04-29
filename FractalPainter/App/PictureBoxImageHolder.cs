using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App
{
    public class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        public PictureBoxImageHolder(ImageSettings imageSettings)
        {
            RecreateImage(imageSettings);
            Dock = DockStyle.Fill;
        }

        public Size GetImageSize()
        {
            FailIfNotInitialized();
            return Image.Size;
        }

        public Graphics StartDrawing()
        {
            FailIfNotInitialized();
            return Graphics.FromImage(Image);
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException("Call PictureBoxImageHolder.RecreateImage before other method call!");
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

        public void SaveImage(string fileName)
        {
            FailIfNotInitialized();
            Image.Save(fileName);
        }
    }
}