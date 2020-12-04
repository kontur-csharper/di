using System.IO;
using System.Linq;

namespace TagCloud.TextProcessing
{
    public class TxtTextReader : ITextReader
    {
        public string[] ReadStrings(string pathToFile)
        {
            return File.ReadLines(pathToFile).ToArray();
        }
    }
}