namespace TagsCloudVisualization.FormAction
{
    public interface IFormAction
    {
        string Category { get; }
        string Name { get; }
        string Description { get; }
        void Perform();
    }
}