using System.Drawing;

namespace TagCloud.Core.Layouting.Lazy
{
    public interface ILazyLayouterFactory
    {
        LayouterType Type { get; }
        ILazyLayouter Create(Size betweenRectsDistance, Point centerPoint);
    }
}