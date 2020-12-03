using System.Collections.Generic;

namespace TagCloud.Core.FileReaders
{
    public interface IFileReader
    {
        FileExtension Extension { get; }
        IEnumerable<string> ReadAllWords(string filePath);
    }
}