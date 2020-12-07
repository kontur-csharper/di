using System;
using System.Collections.Generic;
using RTFToTextConverter;

namespace TagsCloud.FileReader
{
    public class RtfReader : IWordsReader
    {
        public IEnumerable<string> ReadWords(string path)
        {
            return RTFToText.converting().rtfFromFile(path).Split(new string[0], StringSplitOptions.RemoveEmptyEntries);
        }
    }
}