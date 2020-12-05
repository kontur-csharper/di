namespace TagsCloudContainer.TagsCloudVisualization.Interfaces
{
    public interface ILayouterFactory
    {
        public ILayouter GetLayouter(SpiralType type);
    }
}