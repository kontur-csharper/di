using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App
{
    internal class PictureBoxImageHolder : PictureBox, IImageHolder
    {
        private readonly IImageSizeSettingsHolder sizeSettings;
        private readonly IOutputSettingsHolder outputSettings;
        private readonly Lazy<MainForm> mainForm;
        private readonly ICloudVisualizer cloudVisualizer;

        public PictureBoxImageHolder(IImageSizeSettingsHolder sizeSettings,
            IOutputSettingsHolder outputSettings, Lazy<MainForm> mainForm,
            ICloudVisualizer cloudVisualizer)
        {
            this.sizeSettings = sizeSettings;
            this.outputSettings = outputSettings;
            this.mainForm = mainForm;
            this.cloudVisualizer = cloudVisualizer;
        }

        public void GenerateImage()
        {
            mainForm.Value.SetEnabled(false);
            var thread = new Thread(cloudVisualizer.Visualize);
            thread.Start();
        }

        public Graphics StartDrawing()
        {
            FailIfNotInitialized();
            return Graphics.FromImage(Image);
        }

        public void UpdateUi()
        {
            MethodInvoker method = delegate
            {
                mainForm.Value.SetEnabled(true);
                Refresh();
                Application.DoEvents();
            };
            if (mainForm.Value.InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        public void RecreateImage()
        {
            if (Image != null)
                mainForm.Value.ClientSize = new Size(sizeSettings.Width, sizeSettings.Height);
            Image = new Bitmap(sizeSettings.Width,
                sizeSettings.Height, PixelFormat.Format24bppRgb);
        }

        public void SaveImage()
        {
            FailIfNotInitialized();
            Image.Save(outputSettings.OutputFilePath, outputSettings.ImageFormat);
        }

        private void FailIfNotInitialized()
        {
            if (Image == null)
                throw new InvalidOperationException(
                    "Call PictureBoxImageHolder.RecreateImage before other method call!");
        }
    }
}