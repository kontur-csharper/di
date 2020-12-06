namespace WordCloudGenerator
{
    public readonly struct WordFrequency
    {
        public WordFrequency(string word, float frequency)
        {
            Word = word;
            Frequency = frequency;
        }

        public string Word { get; }
        public float Frequency { get; }
    }
}