using System.Collections.Generic;

namespace TagsCloudContainer
{
    public interface IWordsSource
    {
        IEnumerable<(string word, int count)> GetWords();
    }
}