using System.Drawing;

namespace TagsCloud.ColoringAlgorithms
{
    public class BlackColoringAlgorithm : IColoringAlgorithm
    {
        public Color GetNextColor()
        {
            return Color.Black;
        }
    }
}
