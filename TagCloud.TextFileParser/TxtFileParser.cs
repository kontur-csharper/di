using System.Collections.Generic;
using System.IO;

namespace TagCloud.TextFileParser
{
    public class TxtFileParser : ITextFileParser
    {
        public bool TryGetWords(string fileName, string sourceFolderPath, out IEnumerable<string> result)
        {
            result = null;
            if (Path.GetExtension(fileName) != ".txt")
            {
                return false;
            }

            result = File.ReadAllLines(Path.Combine(sourceFolderPath,
                $"{fileName}"));
            return true;
        }
    }
}