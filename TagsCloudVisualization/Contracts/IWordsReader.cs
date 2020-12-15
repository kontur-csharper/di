using System.Collections.Generic;

namespace TagsCloudVisualization.Contracts
{
    public interface IWordsReader
    {
        public IEnumerable<string> GetAllData(string source);
    }
}