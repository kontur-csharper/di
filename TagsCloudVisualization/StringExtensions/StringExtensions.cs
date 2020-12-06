using System.Drawing;

namespace TagsCloudVisualization.StringExtensions
{
    internal static class StringExtensions
    {
        public static Size MeasureString(this string line, Font font)
        {
            using var graphics = Graphics.FromImage(new Bitmap(1, 1));
            return graphics.MeasureString(line, font).ToSize();
        }
    }
}