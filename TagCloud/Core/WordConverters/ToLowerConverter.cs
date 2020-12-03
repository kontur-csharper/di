namespace TagCloud.Core.WordConverters
{
    public class ToLowerConverter : IWordConverter
    {
        public string Convert(string word)
        {
            return word.ToLower();
        }
    }
}