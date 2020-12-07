using System.Drawing;
using TagsCloud.ColoringAlgorithms;

namespace TagsCloud.ImageConfig
{
    public class ImageConfig : IImageConfig
    {
        public Size Size { get; }
        public FontFamily FontFamily { get; }
        public Color BackgroundColor { get; }
        public IColoringAlgorithm ColoringAlgorithm { get; }

        public ImageConfig(
            Size size,
            FontFamily fontFamily,
            Color backgroundColor,
            IColoringAlgorithm coloringAlgorithm)
        {
            Size = size;
            FontFamily = fontFamily;
            BackgroundColor = backgroundColor;
            ColoringAlgorithm = coloringAlgorithm;
        }
    }
}