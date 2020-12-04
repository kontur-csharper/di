using System.Collections.Generic;
using CommandLine;
using YandexMystem.Wrapper.Enums;

namespace TagCloud.UserInterfaces
{
    public class CLIArguments
    {
        [Option("file", Required = true, HelpText = "Set input file path.")]
        public string FilePath { get; set; }
        
        [Option("width", Required = false, HelpText = "Set output image width.", Default = 1000)]
        public int Width { get; set; }

        [Option("height", Required = false, HelpText = "Set output image height.", Default = 1000)]
        public int Height { get; set; }
        
        [Option("colors", Required = false, HelpText = "Set text colors in format \"xxx,xxx,xxx\"",
            Separator = ';')]
        public IEnumerable<string> Colors { get; set; }

        [Option("fgcolor", Required = false, HelpText = "Set background color in format \"xxx,xxx,xxx\"")]
        public string ForegroundColor { get; set; }

        [Option("bgcolor", Required = false, HelpText = "Set background color in format \"xxx,xxx,xxx\"",
            Default = "0 0 0")]
        public string BackgroundColor { get; set; }
        
        [Option("cX", Required = false, HelpText = "Set x coordinate of center.", Default = 500)]
        public int CenterX { get; set; }

        [Option("cY", Required = false, HelpText = "Set y coordinate of center.", Default = 500)]
        public int CenterY { get; set; }

        [Option("pitch", Required = false, HelpText = "Set spiral pitch (angle between points of the spiral).", 
            Default = 4)]
        public int SpiralPitch { get; set; }

        [Option("step", Required = false, HelpText = "Set y coordinate of center.", Default = 0.005)]
        public double SpiralStep { get; set; }

        [Option("font", Required = false, HelpText = "Set output text font.", Default = "Times New Roman")]
        public string FontFamily { get; set; }
        
        [Option("minfont", Required = false, HelpText = "Set min font size.", Default = 12)]
        public int MinFontSize { get; set; }
        
        [Option("maxfont", Required = false, HelpText = "Set max font size.", Default = 48)]
        public int MaxFontSize { get; set; }

        [Option("weightorder", Required = false, HelpText = "Tag cloud place in weight order", Default = true)]
        public bool OrderByWeight { get; set; }

        [Option("outpath", Required = false, HelpText = "Set output file path.")]
        public string OutputPath { get; set; }

        [Option("outname", Required = false, HelpText = "Set output file name.",  Default = "output")]
        public string OutputFileName { get; set; }

        [Option("ex", Required = false, HelpText = "Set output file extention.",  Default = "png")]
        public string Extension { get; set; }
        
        [Option("boringwords", Required = false, HelpText = "Set words to exclude (only russian words)",
            Separator = ',')]
        public IEnumerable<string> BoringWords { get; set; }
        
        [Option("gramparts", Required = false, HelpText = "Set gram parts for tag cloud",
            Separator = ',', Default = new []{GramPartsEnum.Noun, GramPartsEnum.Verb, GramPartsEnum.Adjective})]
        public IEnumerable<GramPartsEnum> GramParts { get; set; }
    }
}