using System.Collections.Generic;

namespace TagsCloud.Infrastructure
{
    public interface IFileAllLinesReader
    {
        HashSet<string> Extensions { get; }

        string[] ReadAllLines(string filePath);
    }
}