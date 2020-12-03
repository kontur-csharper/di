using System.Collections.Generic;
using Xceed.Words.NET;

namespace TagCloud.Core.FileReaders
{
    public class DocReader : IFileReader
    {
        public FileExtension Extension => FileExtension.Doc;

        public IEnumerable<string> ReadAllWords(string path)
        {
            return DocX.Load(path).Text.Split();
        }
    }
}