using System.Collections.Generic;

namespace TagsCloud.FileReaders
{
    interface IFileReader
    {
        public IReadOnlyCollection<string> GetWordsFromFile(string filePath);
    }
}