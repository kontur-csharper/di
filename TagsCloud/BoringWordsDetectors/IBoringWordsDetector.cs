namespace TagsCloud.BoringWordsDetectors
{
    public interface IBoringWordsDetector
    {
        bool IsBoring(string word);
    }
}