namespace MyStem.Wrapper.Workers.Lemmas
{
    public interface ILemmatizer
    {
        string[] GetWords(string text);
    }
}