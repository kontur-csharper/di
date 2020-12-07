namespace TagCloud.Core.Text
{
    public interface IFileWordsReader
    {
        string[] GetWordsFrom(string path);
    }
}