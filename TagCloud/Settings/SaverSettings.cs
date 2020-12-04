namespace TagCloud.Settings
{
    public class SaverSettings
    {
        public string Path { get; }
        public string FileName { get; }
        public string Extension { get; }

        public SaverSettings(string path, string fileName, string extension)
        {
            Path = path;
            FileName = fileName;
            Extension = extension;
        }
    }
}