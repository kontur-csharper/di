namespace TagsCloud.Infrastructure
{
    public class Word
    {
        public Word(string value, double weight)
        {
            Value = value;
            Weight = weight;
        }

        public string Value { get; set; }
        public double Weight { get; set; }
    }
}