namespace TagCloud.Core.Layouting
{
    public interface ILayouterResolver
    {
        ILayouter Get(LayouterType type);
    }
}