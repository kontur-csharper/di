using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordsReader
    {
        List<string> Get(string path);
    }
}