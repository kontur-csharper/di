using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Configs
{
    public class Config : IConfig
    {
        public Font Font { get; private set; }
        public Point Center { get; private set; }

        public Color TextColor { get; private set; }
        public Size ImageSize { get; private set; }
        public HashSet<string> BoringWords { get; private set; }

        public void SetValues(Font font, Point center, Color textColor, Size size, HashSet<string> boringWords)
        {
            Font = font;
            Center = center;
            TextColor = textColor;
            ImageSize = size;
            BoringWords = boringWords;
        }
    }
}