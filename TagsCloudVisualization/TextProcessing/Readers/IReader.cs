namespace TagsCloudVisualization.TextProcessing.Readers
{
    public interface IReader
    {
        string ReadText(string path);
        bool CanReadFile(string path);
    }
}