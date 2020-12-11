using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class StopWords
    {
        private HashSet<string> stopWords = new HashSet<string> { "a", "the", "in", "of", "for", "and", "but" };

        public void Add(string stopWord)
        {
            stopWords.Add(stopWord.ToLower());
        }

        public void Remove(string stopWord)
        {
            stopWords.Remove(stopWord.ToLower());
        }

        public bool Contains(string stopWord)
        {
            return stopWords.Contains(stopWord.ToLower());
        }
    }
}
