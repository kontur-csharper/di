namespace TagsCloud.TextProcessing
{
    public class WordInfo
    {
        public string Word { get; }
        public int Frequence { get; }

        public WordInfo(string value, int frequence)
        {
            Word = value;
            Frequence = frequence;
        }
    }
}
