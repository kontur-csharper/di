using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class Palette
    {
        public Palette(Color primaryColor, Color backgroundColor)
        {
            PrimaryColor = primaryColor;
            BackgroundColor = backgroundColor;
        }

        public Color PrimaryColor { get; set; }
        public Color BackgroundColor { get; set; }
    }
}