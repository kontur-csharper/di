using System.Collections.Generic;
using System.IO;

namespace TagCloud.Core.FileReaders
{
    public class TxtReader : IFileReader
    {
        public FileExtension Extension => FileExtension.Txt;

        public IEnumerable<string> ReadAllWords(string filePath)
        {
            return File.ReadAllText(filePath).Split();
        }
    }
}