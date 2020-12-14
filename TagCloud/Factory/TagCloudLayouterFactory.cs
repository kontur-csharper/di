using TagCloud.Interfaces;
using TagsCloudVisualization;

namespace TagCloud.Factory
{
    public class TagCloudLayouterFactory : ITagCloudLayouterFactory
    {
        public ITagCloudLayouter Get(IPoints points)
        {
            return new CircularCloudLayouter(points);
        }
    }
}