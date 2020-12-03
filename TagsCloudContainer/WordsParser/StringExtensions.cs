namespace TagsCloudContainer.WordsParser
{
    public static class StringExtensions
    {
        public static string NormalizeWord(this string word) => word.ToLower();
    }
}