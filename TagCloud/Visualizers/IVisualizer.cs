using System.Drawing;

namespace TagCloud.Visualizers
{
    public interface IVisualizer<out T>
    {
        T VisualizeTarget { get; }
        void Draw(Graphics graphics);
    }
}
