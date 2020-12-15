using System;
using System.Drawing;
using System.Windows.Forms;

namespace TagsCloudVisualization.Infrastructure.Common
{
    public class SettingsForm<TSettings> : Form
    {
        public SettingsForm(TSettings settings)
        {
            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
            };
            Controls.Add(okButton);
            Controls.Add(new PropertyGrid
            {
                SelectedObject = settings,
                Dock = DockStyle.Fill
            });
            AcceptButton = okButton;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AutoSize = true;
            ClientSize = new Size(400, 450);
        }
    }

    public static class SettingsForm
    {
        public static SettingsForm<TSettings> For<TSettings>(TSettings settings) =>
            new SettingsForm<TSettings>(settings);
    }
}