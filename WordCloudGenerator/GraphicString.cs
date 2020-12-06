namespace WordCloudGenerator
{
    public readonly struct GraphicString
    {
        public GraphicString(string value, float fontSize)
        {
            FontSize = fontSize;
            Value = value;
        }

        public string Value { get; }
        public float FontSize { get; }
    }
}