using CommandLine;

namespace TagCloud.ConsoleAppHelper
{
    // dotnet program
    // -w=1920 -h=1080
    // -f="Times New Roman"
    // -i="words.txt"
    // -o="Examples/test.png"

    public class Options
    {
        [Option('w', "width",
            Default = 1920, HelpText = "width of resulting image")]
        public int Width { get; set; }

        [Option('h', "height",
            Default = 1080, HelpText = "height of resulting image")]
        public int Height { get; set; }

        [Option('f', "font",
            Default = "Times New Roman", HelpText = "font family")]
        public string FontFamily { get; set; }

        [Option('c', Default = "rgb", HelpText = "colors for words")]
        public string Colors { get; set; }

        [Option('i', "input",
            Required = true, HelpText = "input file path")]
        public string Input { get; set; }

        [Option('o', "output",
            Required = true, HelpText = "output file path")]
        public string Output { get; set; }
    }
}