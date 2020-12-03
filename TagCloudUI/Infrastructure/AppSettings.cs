using System;
using System.Collections.Generic;
using CommandLine;
using TagCloud.Core.ColoringAlgorithms;
using TagCloud.Core.LayoutAlgorithms;
using TagCloudUI.UI;

namespace TagCloudUI.Infrastructure
{
    public class AppSettings : IAppSettings
    {
        private readonly Options options;

        public string InputPath => options.InputPath;
        public string OutputPath => options.OutputPath;
        public int ImageWidth => options.ImageWidth;
        public int ImageHeight => options.ImageHeight;
        public LayoutAlgorithmType LayoutAlgorithmType => options.LayoutAlgorithmType;
        public ColoringTheme ColoringTheme => options.ColoringTheme;
        public string FontName => options.FontName;
        public string ImageFormat => options.ImageFormat;
        public int WordsCount => options.WordsCount;

        private AppSettings(Options options)
        {
            this.options = options;
        }

        public static AppSettings Create(IEnumerable<string> args)
        {
            var parserResult = Parser.Default.ParseArguments<Options>(args);
            var parsedOptions = (parserResult as Parsed<Options>)?.Value;

            if (parsedOptions == null)
                throw new ArgumentException("An error occurred while parsing the parameters");

            return new AppSettings(parsedOptions);
        }
    }
}