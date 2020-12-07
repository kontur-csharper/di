using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI
{
    public class MainForm : Form
    {
        public MainForm(IUiAction[] actions, PictureBoxImageHolder pictureBox, ImageSettings imageSettings)
        {
            ClientSize = new Size(imageSettings.Width, imageSettings.Height);

            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems().OrderBy(x => x.Text.Length).ToArray());
            Controls.Add(mainMenu);

            pictureBox.RecreateImage(imageSettings);
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tags Cloud Painter";
        }
    }
}