namespace TagsCloud.App
{
    public interface IWordsFilter
    {
        bool Validate(string word);
    }
}