using System.Collections.Generic;

namespace TagCloud.TextFileParser
{
    public class FileParser : ITextFileParser
    {
        private readonly IEnumerable<ITextFileParser> implementations;

        public FileParser(IEnumerable<ITextFileParser> implementations)
        {
            this.implementations = implementations;
        }

        public bool TryGetWords(string fileName, string sourceFolderPath, out IEnumerable<string> result)
        {
            result = null;
            foreach (var parser in implementations)
            {
                if (parser.TryGetWords(fileName, sourceFolderPath, out result))
                {
                    return true;
                }
            }

            return false;
        }
    }
}