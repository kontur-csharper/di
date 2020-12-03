using System.Collections.Generic;
using CommandLine;

namespace TagsCloudContainer
{
    public class CommandLineOptions
    {
        [Option("width", Required = false, HelpText = "Set output image width.", Default = 500)]
        public int Width { get; set; }

        [Option("height", Required = false, HelpText = "Set output image height.", Default = 500)]
        public int Height { get; set; }

        [Option("font", Required = false, HelpText = "Set output text font.", Default = "Times New Roman")]
        public string FontFamily { get; set; }

        [Option("file", Required = true, HelpText = "Set input file path.")]
        public string FilePath { get; set; }

        [Option("outdir", Required = true, HelpText = "Set output file path.")]
        public string OutputDirectory { get; set; }

        [Option("outname", Required = true, HelpText = "Set output file name.")]
        public string OutputFileName { get; set; }

        [Option("outext", Required = false, HelpText = "Set output file extension.", Default = ".png")]
        public string OutputFileExtension { get; set; }

        [Option("color", Required = false,
            HelpText = "Set font color. Possible value - random or color in argb(xxx,xxx,xxx,xxx)." +
                       "Example: -color argb(0,0,0,0)",
            Default = "random")]
        public string FontColor { get; set; }

        [Option("mystem", Required = false, HelpText = "Set mystem path.")]
        public string MystemLocation { get; set; }

        [Option("boring_words", Required = false, Separator = ' ')]
        public IEnumerable<string> BoringWords { get; set; }
    }
}