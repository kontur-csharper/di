using System.Drawing;

namespace TagsCloudContainer
{
    public interface IColorProvider
    {
        Color GetNextColor();
    }
}