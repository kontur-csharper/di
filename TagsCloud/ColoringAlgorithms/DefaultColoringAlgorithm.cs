using System.Drawing;

namespace TagsCloud.ColoringAlgorithms
{
    class DefaultColoringAlgorithm : IColoringAlgorithm
    {
        private Color _color;
        public DefaultColoringAlgorithm(Color color)
        {
            _color = color;
        }
        public Color GetNextColor()
        {
            return _color;
        }
    }
}
