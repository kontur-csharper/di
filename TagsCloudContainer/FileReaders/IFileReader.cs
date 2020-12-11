using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IFileReader
    {
        string Format { get; set; }
        IEnumerable<string> ReadAllLines(string filePath);
    }
}