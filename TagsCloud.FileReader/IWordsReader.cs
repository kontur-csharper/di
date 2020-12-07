using System.Collections.Generic;

namespace TagsCloud.FileReader
{
    public interface IWordsReader
    {
        IEnumerable<string> ReadWords(string path);
    }
}