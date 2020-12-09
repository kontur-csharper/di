using TagsCloudContainer.Infrastructure.TextAnalyzer;
using YandexMystem.Wrapper;

namespace TagsCloudContainer.App.TextAnalyzer
{
    public class ToInitialFormNormalizer : IWordNormalizer
    {
        private readonly Mysteam mysteam;

        public ToInitialFormNormalizer(Mysteam mysteam)
        {
            this.mysteam = mysteam;
        }

        public string NormalizeWord(string word)
        {
            return mysteam.GetWords(word)[0].Lexems[0].Lexeme;
        }
    }
}
