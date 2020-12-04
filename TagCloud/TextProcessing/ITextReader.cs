namespace TagCloud.TextProcessing
{
    public interface ITextReader
    {
        string[] ReadStrings(string pathToFile);
    }
}