using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Settings
{
    public class DrawerSettings
    {
        public Size ImageSize { get; set; } = new Size(1000, 1000);
        public List<Color> Colors { get; set; } = new List<Color>();
        public Color ForegroundColor { get; set; } = Color.FromArgb(0, 0, 0, 0);
        public Color BackgroundColor { get; set; } = Color.Black;
        public string FontFamily { get; set; } = "Times New Roman";
        public int MaxFontSize { get; set; } = 24;
        public int MinFontSize { get; set; } = 48;
        public bool OrderByWeight { get; set; } = true;
    }
}