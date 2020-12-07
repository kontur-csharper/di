using System.Drawing;

namespace TagCloud.Core.Layouting.Lazy
{
    public class LazyCircularLayouterFactory : ILazyLayouterFactory
    {
        public LayouterType Type => LayouterType.Circular;

        public ILazyLayouter Create(Size betweenRectsDistance, Point centerPoint) =>
            new CircularLazyLayouter(centerPoint, betweenRectsDistance);
    }
}