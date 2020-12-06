using System.Collections.Generic;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordsFrequency
    {
        Dictionary<string, int> GetWordsFrequency(string text);
    }
}