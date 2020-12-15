using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TagsCloudVisualization.Contracts;

namespace TagsCloudVisualization.Infrastructure
{
    public class FileWordsReader : IWordsReader
    {
        public IEnumerable<string> GetAllData(string fileName) => Regex.Split(File.ReadAllText(fileName), @"\W+");
    }
}