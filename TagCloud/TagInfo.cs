namespace TagCloud
{
    public class TagInfo
    {
        public readonly string Value;
        public readonly double Weight;
        
        public TagInfo(string value, double weight)
        {
            Value = value;
            Weight = weight;
        }
    }
}