namespace TagCloud.TextConverters.TextReaders
{
    public interface ITextReader
    {
        public string Extension { get; }

        public string ReadText(string path);
    }
}
