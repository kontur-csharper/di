using System.Collections.Generic;

namespace TagsCloudContainer.WordsParser
{
    public interface IFilter
    {
        public HashSet<string> RemoveBoringWords(HashSet<string> words);
    }
}