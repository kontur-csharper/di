namespace TagsCloud.TextProcessing.Converters
{
    public class LowerCaseConverter : IWordConverter
    {
        public string Convert(string word) => word.ToLower();
    }
}
