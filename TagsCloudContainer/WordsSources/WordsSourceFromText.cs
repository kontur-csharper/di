using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudContainer
{
    public class WordsSourceFromText : IWordsSource
    {
        public readonly string Text;

        public WordsSourceFromText(string text)
        {
            Text = text;
        }

        public IEnumerable<(string word, int count)> GetWords()
        {
            var words = Regex.Matches(Text, @"\b\w+\b").Cast<Match>().Select(m => m.Value);
            return words.GroupBy(w => w).Select(g => (g.Key, g.Count()));
        }
    }
}