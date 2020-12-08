using System;
using System.Drawing;
using System.Windows.Forms;
using TagsCloud.Infrastructure;

namespace TagsCloud.GUI
{
    class SettingsForm : Form
    {
        public SettingsForm(IImageHolder holder)
        {
            var settings = holder.Settings;
            var oldBackgroundColor = settings.Palette.BackgroundColor;
            var oldTextColor = settings.Palette.TextColor;
            var oldFont = settings.Font;
            var oldWidth = settings.Width;
            var oldHeight = settings.Height;

            ClientSize = new Size(300, 400);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            AutoSize = true;

            var propGrid = new PropertyGrid {SelectedObject = settings, Dock = DockStyle.Fill};
            Controls.Add(propGrid);

            var acceptButton = new Button {Text = "Применить", Dock = DockStyle.Bottom, AutoSize = true};
            acceptButton.Click += (sender, args) => Close();
            Controls.Add(acceptButton);

            var abortButton = new Button {Text = "Отмена", Dock = DockStyle.Bottom, AutoSize = true};
            abortButton.Click += (sender, args) =>
            {
                settings.Width = oldWidth;
                settings.Height = oldHeight;
                settings.Palette.TextColor = oldTextColor;
                settings.Palette.BackgroundColor = oldBackgroundColor;
                settings.Font = oldFont;
                Close();
            };
            Controls.Add(abortButton);
            
        }

        protected override void OnShown(EventArgs e)
        {
            Text = "Настройки изображения";
            base.OnShown(e);
        }
    }
}
