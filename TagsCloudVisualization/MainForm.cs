using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TagsCloudVisualization.Canvases;
using TagsCloudVisualization.FormAction;

namespace TagsCloudVisualization
{
    public class MainForm : Form
    {
        public MainForm(IEnumerable<IFormAction> actions, ICanvas canvas)
        {
            var mainMenu = new MenuStrip();
            mainMenu.Items.AddRange(actions.ToMenuItems());
            Controls.Add(mainMenu);
            Controls.Add((Canvas)canvas);
            Size = canvas.GetImageSize();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Text = "Tag Cloud Visualizer";
        }
    }
}