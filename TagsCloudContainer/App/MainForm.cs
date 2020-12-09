using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App
{
    internal class MainForm : Form
    {
        private readonly MenuStrip mainMenu;

        public MainForm(IEnumerable<IUiAction> actions,
            PictureBoxImageHolder pictureBox, IImageSizeSettingsHolder sizeSettings)
        {
            ClientSize = new Size(sizeSettings.Width,
                sizeSettings.Height);
            MaximizeBox = false;
            MinimizeBox = false;
            mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToArray().ToMenuItems());
            Controls.Add(mainMenu);
            pictureBox.RecreateImage();
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "TagsCloud Painter";
        }

        public void SetEnabled(bool enabled)
        {
            mainMenu.Enabled = enabled;
        }
    }
}