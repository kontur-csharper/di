using MatthiWare.CommandLine.Core.Attributes;

namespace Cloud.ClientUI
{
    public class Arguments //TODO add file input and file output
    {
        [Name("fn", "font name")]
        [Description("Text font name")]
        public string FontName { get; set; }

        [Name("fs", "font size")]
        [Description("Text font size")]
        public string FontSize { get; set; }

        [Name("ws", "width size")]
        [Description("Image width")]
        public string ImageWidth { get; set; }

        [Name("hs", "height size")]
        [Description("Image width")]
        public string ImageHeight { get; set; }

        [Name("x", "x value")]
        [Description("x value of center")]
        public string XValue { get; set; }

        [Name("y", "y value")]
        [Description("y value of center")]
        public string YValue { get; set; }

        [Name("c", "color")]
        [Description("Color of words")]
        public string Color { get; set; }

        [Name("b", "boring words")]
        [Description("Words thas should not be added to cloud")]
        public string BoringWords { get; set; }

        [Name("i", "input file")]
        [Description("Input file name")]
        public string InputFileName { get; set; }

        [Name("o", "output file")]
        [Description("Output file name")]
        public string OutputFileName { get; set; }
    }
}