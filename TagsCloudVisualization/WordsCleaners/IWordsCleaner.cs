using System.Collections.Generic;

namespace TagsCloudVisualization.WordsCleaners
{
    public interface IWordsCleaner
    {
        public List<string> CleanWords(IEnumerable<string> words);
        public void AddBoringWords(HashSet<string> boringWords);
    }
}