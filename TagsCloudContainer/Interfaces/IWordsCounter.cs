using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsCounter
    {
        Dictionary<string, int> CountWords(string[] words);
    }
}