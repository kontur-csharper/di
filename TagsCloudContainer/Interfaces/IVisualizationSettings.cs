using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface IVisualizationSettings
    {
        Size ImageSize { get; }
        Color BackgroundColor { get; }
        Color TextColor { get; }
        Font Font { get; }
    }
}