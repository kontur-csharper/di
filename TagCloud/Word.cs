namespace TagCloud
{
    public class Word
    {
        public string Value { get; }
        public double Weight { get; }

        public Word(string value, double weight)
        {
            Value = value;
            Weight = weight;
        }
    }
}