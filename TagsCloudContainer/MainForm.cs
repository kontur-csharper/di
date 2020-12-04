using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudContainer.UiActions;

namespace TagsCloudContainer
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions, AppSettings settings, PictureBoxImageHolder imageHolder)
        {
            var imageSettings = settings.ImageSettings;
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);


            imageHolder.RecreateImage(imageSettings);
            imageHolder.Dock = DockStyle.Fill;
            Controls.Add(imageHolder);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag Cloud Visualization";
        }
    }
}