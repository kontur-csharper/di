using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class FileReaderProvider
    {
        private readonly IEnumerable<IFileAllLinesReader> fileReaders;

        public FileReaderProvider(IEnumerable<IFileAllLinesReader> fileReaders)
        {
            this.fileReaders = fileReaders;
        }

        public Result<IFileAllLinesReader> GetFileReader(string extension) =>
            fileReaders.FirstOrDefault(x => x.Extensions.Contains(extension))?.AsResult()
            ?? Result.Fail<IFileAllLinesReader>("Can't read file in this format");
    }
}