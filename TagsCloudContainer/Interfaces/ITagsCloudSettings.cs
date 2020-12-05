using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagsCloudSettings
    {
        Color BackgroundColor { get; }
        int ImageWidth { get; }
        int ImageHeight { get; }
    }
}