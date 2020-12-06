using System.Drawing;

namespace TagsCloudVisualization.AppSettings
{
    public class PaintingSettings
    {
        public Color TextColor { get; set; } = Color.Orange;
        public Color BackgroundColor { get; set; } = Color.Black;

        public bool EnabledMultipleColors { get; set; } = true;
        public bool DrawTextBoundingZone { get; set; } = false;
        public bool EnableAnimation { get; set; } = true;

        public Color[] Colors { get; set; } = 
        {
            Color.MediumVioletRed, Color.Orange, Color.White,
            Color.Red, Color.LawnGreen, Color.SpringGreen
        };
    }
}