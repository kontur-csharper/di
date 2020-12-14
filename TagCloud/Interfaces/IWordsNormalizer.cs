using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordsNormalizer
    {
        List<string> NormalizeWords(List<string> words, HashSet<string> boringWords);
    }
}