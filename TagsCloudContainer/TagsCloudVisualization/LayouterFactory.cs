using System.Collections.Generic;
using System.Linq;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class LayouterFactory : ILayouterFactory
    {
        private readonly IEnumerable<ISpiral> spirals;

        public LayouterFactory(IEnumerable<ISpiral> spirals)
        {
            this.spirals = spirals;
        }

        public ILayouter GetLayouter(SpiralType type)
        {
            return new CloudLayouter(spirals.First(x => x.Type == type));
        }
    }
}