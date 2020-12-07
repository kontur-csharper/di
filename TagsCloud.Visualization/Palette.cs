using System.Drawing;

namespace TagsCloud.Visualization
{
    public class Palette
    {
        public Color ForeColor { get; set; } = Color.Yellow;
        public Color[] ForeColors { get; set; } = {Color.Yellow, Color.Red, Color.LightGreen};
        public Color BackgroundColor { get; set; } = Color.Black;
    }
}