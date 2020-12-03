using CommandLine;
using TagCloud.Core.ColoringAlgorithms;
using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.UI
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input data path")]
        public string InputPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output path")]
        public string OutputPath { get; set; }

        [Option('w', "width", Default = 1200, HelpText = "Image width")]
        public int ImageWidth { get; set; }

        [Option('h', "height", Default = 1200, HelpText = "Image height")]
        public int ImageHeight { get; set; }

        [Option('a', "algo", Default = LayoutAlgorithmType.Circular, HelpText = "Layout algorithm type")]
        public LayoutAlgorithmType LayoutAlgorithmType { get; set; }

        [Option('t', "theme", Default = ColoringTheme.Rainbow, HelpText = "Coloring theme")]
        public ColoringTheme ColoringTheme { get; set; }

        [Option('f', "font", Default = "Arial", Required = false, HelpText = "Tags font")]
        public string FontName { get; set; }

        [Option('e', "extension", Default = "png", HelpText = "Image format")]
        public string ImageFormat { get; set; }

        [Option('c', "count", Default = 40, HelpText = "Words count")]
        public int WordsCount { get; set; }
    }
}