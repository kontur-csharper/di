using System.Drawing;
using CommandLine;

namespace HomeExercise.Options
{
    [Verb("options", HelpText = "Input options.")]
    public class Options
    {
        [Option("words", Required = true, HelpText = "Input files with words.")]
        public string WordsPath { get; set; }
    
        [Option( "boring", Required = false, HelpText = "Input files with boring words.")]
        public string BoringPath { get; set; }
        
        [Option("format", Default = "png",Required = false, HelpText = "Input image format.")]
        public string Format { get; set; }
        
        [Option('w', "wight", Default = 3000,Required = false, HelpText = "Input image wight.")]
        public int Wight { get; set; }
        
        [Option('h', "height", Default = 3000,Required = false, HelpText = "Input image height.")]
        public int Height { get; set; }
        
        [Option("font",Default = "Microsoft Sans Serif",Required = false, HelpText = "Input font family.")]
        public string Font { get; set; }
        
        [Option("imageName", Default = "name", Required = false, HelpText = "Input image file name.")]
        public string ImageName { get; set; }
        
        [Option("color", Default = 22,Required = false, HelpText = "Input color enum.")]
        public KnownColor Color { get; set; }
        
        [Option('c',"coefficient",Default = 40, Required = false, HelpText = "Input coefficient.")]
        public int Coefficient { get; set; }
        
        [Option("centerX",Default = 0,Required = false, HelpText = "Input center point.")]
        public int CenterX { get; set; }
        [Option("centerX",Required = false, HelpText = "Input center point.")]
        public int CenterY { get; set; }
    }
}