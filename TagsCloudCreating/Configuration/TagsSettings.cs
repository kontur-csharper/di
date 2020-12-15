using System.Drawing;
using TagsCloudCreating.Contracts;
using TagsCloudCreating.Core.ColorizeAlgorithms;

namespace TagsCloudCreating.Configuration
{
    public class TagsSettings
    {
        public Font Font { get; set; } = SystemFonts.DefaultFont;
        public IColorizer Colorizer { get; set; } = new RandomColorizer();
    }
}