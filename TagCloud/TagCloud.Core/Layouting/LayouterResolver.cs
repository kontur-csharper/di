using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.Layouting
{
    public class LayouterResolver : ILayouterResolver
    {
        private readonly Dictionary<LayouterType, ILayouter> layouters;

        public LayouterResolver(IEnumerable<ILayouter> layouters)
        {
            this.layouters = layouters.ToDictionary(l => l.Type);
        }

        public ILayouter Get(LayouterType type) => layouters[type];
    }
}