namespace TagsCloud.FileReader
{
    public interface IReaderFactory
    {
        IWordsReader GetReader(string extension);
    }
}