namespace TagCloud.TextProcessing
{
    public interface IWordParser
    {
        string[] GetWords(string fileName);
    }
}