using System.Collections.Generic;

namespace TagsCloud.WordReaders
{
    public interface IWordReader
    {
        IEnumerable<string> ReadWords();
    }
}