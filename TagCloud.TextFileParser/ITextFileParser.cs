using System.Collections.Generic;

namespace TagCloud.TextFileParser
{
    public interface ITextFileParser
    {
        public bool TryGetWords(string fileName, string sourceFolderPath, out IEnumerable<string> result);
    }
}