using System;
using System.ComponentModel;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class Palette
    {
        private Color backgroundColor = Color.FromArgb(35, 35, 35);

        [DisplayName("Цвет фона")]
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                ImageSettings.OnSettingsChange(EventArgs.Empty);
            }
        } 

        private Color textColor = Color.FromArgb(210, 198, 198);

        [DisplayName("Цвет текста")]
        public Color TextColor
        {
            get => textColor;
            set
            {
                textColor = value;
                ImageSettings.OnSettingsChange(EventArgs.Empty);
            }
        }
    }
}