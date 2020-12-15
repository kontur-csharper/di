namespace TagCloud.TextConverters.WordExcluders
{
    public interface IWordExcluder
    {
        public bool MustBeExclude(string word);
    }
}
