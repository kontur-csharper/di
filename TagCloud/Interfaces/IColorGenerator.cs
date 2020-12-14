using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface IColorGenerator
    {
        Color GetNextColor();
    }
}