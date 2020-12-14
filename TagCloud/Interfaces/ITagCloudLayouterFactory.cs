using TagsCloudVisualization;

namespace TagCloud.Interfaces
{
    public interface ITagCloudLayouterFactory
    {
        ITagCloudLayouter Get(IPoints points);
    }
}