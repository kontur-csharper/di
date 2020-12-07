using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloud.FileReader
{
    public class TxtReader : IWordsReader
    {
        public IEnumerable<string> ReadWords(string path)
        {
            return File.ReadAllText(path).Split(new string[0], StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
