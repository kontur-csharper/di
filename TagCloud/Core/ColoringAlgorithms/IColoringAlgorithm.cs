using System.Drawing;

namespace TagCloud.Core.ColoringAlgorithms
{
    public interface IColoringAlgorithm
    {
        ColoringTheme Theme { get; }
        Color GetNextColor(Tag tag);
    }
}