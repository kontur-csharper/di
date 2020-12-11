using System.IO;

namespace TagsCloud.App
{
    public class FileReader
    {
        private readonly FileReaderProvider fileReaderProvider;

        public FileReader(FileReaderProvider fileReaderProvider)
        {
            this.fileReaderProvider = fileReaderProvider;
        }

        public Result<string[]> ReadLines(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return extension == string.Empty
                ? Result.Fail<string[]>("Please write extension of file")
                : fileReaderProvider.GetFileReader(extension)
                    .Then(x => x.ReadAllLines(filePath));
        }
    }
}