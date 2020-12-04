namespace TagCloud.Settings
{
    public class FileReaderSettings
    {
        public string FilePath { get; }

        public FileReaderSettings(string filePath)
        {
            FilePath = filePath;
        }
    }
}