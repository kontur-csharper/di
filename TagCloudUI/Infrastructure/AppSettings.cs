﻿using System;
using System.Collections.Generic;
using CommandLine;
using TagCloud.Core.ColoringAlgorithms;
using TagCloud.Core.LayoutAlgorithms;
using TagCloudUI.UI;

namespace TagCloudUI.Infrastructure
{
    public class AppSettings
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

        public AppSettings(IEnumerable<string> args)
        {
            var parserResult = Parser.Default.ParseArguments<Options>(args);
            options = (parserResult as Parsed<Options>)?.Value;

            if (options == null)
                throw new ArgumentException("An error occurred while parsing the parameters");
        }
    }
}