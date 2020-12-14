using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface IColorGeneratorFactory
    {
        IColorGenerator Get(Color[] colors);
    }
}