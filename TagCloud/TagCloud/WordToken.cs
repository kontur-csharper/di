namespace TagCloud
{
    public class WordToken
    {
        public WordToken(string word, int frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public string Word { get; set; }
        public int Frequency { get; set; }
    }
}