using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloud.App
{
    public class TxtFileReader : FileReader
    {
        public override HashSet<string> AvailableFileTypes { get; } = new HashSet<string> {"txt"};

        protected override IEnumerable<string> ReadWordsInternal(string fileName)
        {
            return File.ReadAllLines(fileName).SelectMany(line => splitRegex.Split(line));
        }
    }
}