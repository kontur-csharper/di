using System.Collections.Generic;

namespace TagsCloud.App
{
    public interface IFileReader
    {
        HashSet<string> AvailableFileTypes { get; }
        IEnumerable<string> ReadWords(string fileName);
    }
}