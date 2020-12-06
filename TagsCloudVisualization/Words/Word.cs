namespace TagsCloudVisualization.Words
{
    public class Word
    {
        public string Value { get; }
        public int Weight { get; }
        
        public Word(string value, int weight)
        {
            Value = value;
            Weight = weight;
        }        
    }
}