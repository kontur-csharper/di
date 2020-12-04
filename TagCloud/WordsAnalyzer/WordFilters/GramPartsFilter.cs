using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using YandexMystem.Wrapper;
using YandexMystem.Wrapper.Enums;

namespace TagCloud.WordsAnalyzer.WordFilters
{
    public class GramPartsFilter : IWordFilter
    {
        private Regex cyrillicRegex;
        private Mysteam mystem;

        private HashSet<GramPartsEnum> allowedGramParts;
        
        public GramPartsFilter(params GramPartsEnum[] allowedGramParts)
        {
            this.allowedGramParts = allowedGramParts.ToHashSet();
            cyrillicRegex = new Regex("\\p{IsCyrillic}");
            mystem = new Mysteam();
        }
        
        public bool ShouldExclude(string word)
        {
            if (!IsCyrillicWord(word))
                return false;
            var models = mystem.GetWords(word);
            if (models.Count != 1)
                return false;

            var model = models[0];
            return !allowedGramParts.Contains(model.Lexems[0].GramPart);
        }

        private bool IsCyrillicWord(string word)
        {
            return cyrillicRegex.IsMatch(word);
        }
    }
}