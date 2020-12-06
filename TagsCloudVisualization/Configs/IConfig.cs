using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization.Configs
{
    public interface IConfig
    {
        Font Font { get; }
        Point Center { get; }

        Color TextColor { get; }

        Size ImageSize { get; }

        HashSet<string> BoringWords { get; }

        public void SetValues(Font font, Point center, Color textColor, Size size, HashSet<string> boringWords);
    }
}