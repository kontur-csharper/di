using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.TextAnalyzer;
using YandexMystem.Wrapper;

namespace TagsCloudContainer.App.TextAnalyzer
{
    public class PartOfSpeechFilter : IWordFilter
    {
        private readonly IFilteringWordsSettingsHolder settings;
        private readonly Mysteam mysteam;

        public PartOfSpeechFilter(IFilteringWordsSettingsHolder settings, Mysteam mysteam)
        {
            this.settings = settings;
            this.mysteam = mysteam;
        }

        public bool IsBoring(string word)
        {
            return settings.BoringGramParts.Contains(mysteam.GetWords(word)[0].Lexems[0].GramPart);
        }
    }
}
