using System.Collections.Generic;

namespace TagsCloud.TextProcessing.TextReaders
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadWords(string path);
        bool CanRead(string path);
    }
}
