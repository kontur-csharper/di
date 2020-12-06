using System.Drawing;
using System.Drawing.Imaging;

namespace HomeExercise.settings
{
    public class PainterSettings
    {
        public Color Color { get; }
        public int Width { get; }
        public int Height { get; }
        public string FileName { get; }
        public ImageFormat Format { get; }
        
        public PainterSettings(int width, int height, string fileName, ImageFormat format, Color color)
        {
            Width = width;
            Height = height;
            FileName = fileName;
            Format = format;
            Color = color;
        }
    }
}