using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Core.Layouting.Lazy
{
    public class LazyLayouterAdapter : ILayouter
    {
        private readonly ILazyLayouterFactory lazyLayouterFactory;

        public LazyLayouterAdapter(ILazyLayouterFactory lazyLayouterFactory)
        {
            this.lazyLayouterFactory = lazyLayouterFactory;
        }

        public LayouterType Type => lazyLayouterFactory.Type;

        public IEnumerable<Rectangle> PutAll(Point centerPoint,
            Size betweenRectanglesDistance,
            IEnumerable<Size> words)
        {
            var layouter = lazyLayouterFactory.Create(betweenRectanglesDistance, centerPoint);
            return words.Select(layouter.PutNextRectangle);
        }
    }
}