using System.Drawing;

namespace TagsCloudContainer.Common
{
    public interface IColorSettings
    {
        Color BackgroundColor { get; set; }
        Color GetNextColor();
    }
}