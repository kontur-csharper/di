using System.Drawing;

namespace TagsCloud.TextProcessing.WordsConfig
{
    public class WordConfig
    {
        public Font Font { get; set; }
        public Color Color { get; set; }
        public string Path { get; set; }
        public string[] FilersNames { get; set; }
        public string[] ConvertersNames { get; set; }
        public string LayouterName { get; set; }
        public string TagGeneratorName { get; set; }

    }
}
