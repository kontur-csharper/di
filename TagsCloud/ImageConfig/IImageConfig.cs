using System.Drawing;
using TagsCloud.ColoringAlgorithms;

namespace TagsCloud.ImageConfig
{
    public interface IImageConfig
    {
        public Size Size { get; }
        public FontFamily FontFamily { get; }
        public Color BackgroundColor { get; }
        public IColoringAlgorithm ColoringAlgorithm { get; }
    }
}