using WeCantSpell.Hunspell;
using System.Collections.Generic;

namespace TagCloud.TextConverters.WordExcluders
{
    public class WordsExcluder : IWordExcluder
    {
        private static readonly HashSet<string> pronouns = new HashSet<string>()
        { 
            "i", "me", "my", 
            "he", "his", "him",
            "she", "her",
            "it" 
        };
        private static readonly HashSet<string> prepositions = new HashSet<string>()
        {
            "a", "an", "at"
        };
        public bool MustBeExclude(string word) => 
            pronouns.Contains(word) ||
            prepositions.Contains(word);
    }
}
