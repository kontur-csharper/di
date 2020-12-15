using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.Contracts;
using TagsCloudVisualization.Infrastructure;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudVisualization.App
{
    public partial class MainForm : Form
    {
        public MainForm(
            ImageSettings imageSettings,
            TagsCloudPictureHolder tagsCloudPictureHold,
            IEnumerable<IMenuItem> items)
        {
            Name = nameof(MainForm);
            RecreateImage(imageSettings);

            MainMenuStrip = new MenuStrip();
            MainMenuStrip.Items.AddRange(items.ToMenuItems());

            Controls.Add(MainMenuStrip);
            Controls.Add(tagsCloudPictureHold);
        }

        private void RecreateImage(ImageSettings imageSettings) =>
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag's cloud visualizer";
        }
    }
}