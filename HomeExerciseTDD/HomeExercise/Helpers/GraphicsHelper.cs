using System.Drawing;

namespace HomeExercise.Helpers
{
    public static class GraphicsHelper
    {
        public static Size MeasureString(string text, Font font)
        {
            using var image = new Bitmap(1, 1);
            using var g = Graphics.FromImage(image);
            var result = g.MeasureString(text, font).ToSize();

            return result;
        }
    }
}