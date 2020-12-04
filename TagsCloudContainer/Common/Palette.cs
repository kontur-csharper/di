using System.Drawing;

namespace TagsCloudContainer.Common
{
    public class Palette : IColorSettings
    {
        public Color TextColor { get; set; } = Color.Orange;
        public Color BackgroundColor { get; set; } = Color.Black;

        public Color GetNextColor()
        {
            return TextColor;
        }
    }
}