using TagsCloudContainer.TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TagsCloudContainer
{
    public class WordValidator : IWordValidator
    {
        public bool IsValidWord(string word)
        {
            return word.Length > 2;
        }
    }
}