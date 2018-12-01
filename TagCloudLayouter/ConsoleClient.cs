﻿using System;
using System.Drawing;
using CommandLine;

namespace TagCloudLayouter
{
    public class ConsoleClient : IUserInterface
    {
        private class Options
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }

            [Option('c', "count", Default = 20, HelpText = "Input count of tags in cloud.")]
            public int Count { get; set; }

            [Option("font-name", Default = "Times New Roman", HelpText = "Input font name.")]
            public string FontName { get; set; }

            [Option("font-size", Default = 40.0f, HelpText = "Input font size.")]
            public float FontSize { get; set; }

            [Option('n', "name", Default = "Cloud", HelpText = "Input file name.")]
            public string FileName { get; set; }

            [Option("color", Default = "Black", HelpText = "Input color.")]
            public string Color{ get; set; }

            [Option("out-path", HelpText = "Input path to directory to save image.")]
            public string OutPath { get; set; }

            [Value(0, HelpText = "Path to directory to save.")]
            public string PathToSave { get; set; }
        }

        public Config GetConfig(string[] args)
        {
            var center = new Point(500, 500);
            var inputFile = "";
            var count = 20;
            var fileName = "SimpleCloud";
            var outPath = "";
            var font = new Font("Times New Roman", 40.0f);
            var color = Color.Black;

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var fontName = o.FontName;
                    var fontSize = o.FontSize;
                    count = o.Count;
                    inputFile = o.PathToSave;
                    fileName = o.FileName;
                    outPath = o.OutPath ?? Environment.CurrentDirectory;
                    font = new Font(fontName, fontSize);
                    color = Color.FromName(o.Color);
                });

            return new Config(
                center,
                inputFile, 
                count,
                font,
                fileName,
                outPath,
                color);
        }
    }
}
