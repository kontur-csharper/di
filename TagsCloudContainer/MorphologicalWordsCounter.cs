using System;
using System.Collections.Generic;
using System.Linq;
using WeCantSpell.Hunspell;

namespace TagsCloudContainer
{
    public class MorphologicalWordsCounter : IWordsCounter
    {
        private readonly IEnumerable<PartOfSpeech> excludedPartsOfSpeech = Enumerable.Empty<PartOfSpeech>();
        private readonly WordList wordList;

        public MorphologicalWordsCounter(WordList wordList)
        {
            this.wordList = wordList;
        }
        
        public MorphologicalWordsCounter(WordList wordList, IEnumerable<PartOfSpeech> excludedPartsOfSpeech)
        {
            this.excludedPartsOfSpeech = excludedPartsOfSpeech;
            this.wordList = wordList;
        }

        public Dictionary<string, int> CountWords(string[] words)
        {
            var dict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                var preparedWord = PrepareWord(word);
                if (!MustBeCounted(preparedWord))
                    continue;
                if (dict.ContainsKey(preparedWord))
                    dict[preparedWord]++;
                else
                    dict[preparedWord] = 1;
            }

            return dict;
        }

        private string PrepareWord(string word)
        {
            var wordInLowercase = word.ToLower();
            var spellDetails = wordList.CheckDetails(wordInLowercase);
            return spellDetails.Root ?? wordInLowercase;
        }

        private bool MustBeCounted(string word)
        {
            var details = wordList[word];
            if (details.Length == 0)
                return false;
            
            var morphs = details[0].Morphs;
            return morphs.IsEmpty ||
                   !excludedPartsOfSpeech.Contains(GetPartOfSpeech(morphs));
        }

        private static PartOfSpeech GetPartOfSpeech(MorphSet morphs)
        {
            var poParameter = morphs.First(arg => arg.StartsWith("po:"));
            return Enum.Parse<PartOfSpeech>(poParameter.Substring(3), true);
        }
    }
}