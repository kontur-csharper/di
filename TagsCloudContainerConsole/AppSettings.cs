using System;
using System.Collections.Generic;
using System.Drawing;
using CommandLine;

namespace TagsCloudContainer
{
    public class AppSettings : ITagsCloudSettings
    {
        [Option("backgroundColor", Default = "White")]
        public string BackgroundColorName { get; set; }

        [Option("imageWidth", Default = 500)]
        public int ImageWidth { get; set; }

        [Option("imageHeight", Default = 500)]
        public int ImageHeight { get; set; }

        [Option("imagePath", Default = "TagsCloud.png")]
        public string ImagePath { get; set; }

        [Option("file", Required = true)]
        public string PathToFile { get; set; }

        [Option("excludedPo", Separator = ',',
            Default = new[] {"pronoun", "conjunction", "determiner", "preposition"})]
        public IEnumerable<string> ExcludedPartsOfSpeechNames { get; set; }

        [Option("font", Default = "Arial")]
        public string FontName { get; set; }

        [Option("fontColor", Default = "Black")]
        public string FontColorName { get; set; }

        [Option("angleStep", Default = 10)]
        public double AngleStepInDegrees { get; set; }

        [Option("shiftFactor", Default = 18 / Math.PI)]
        public double ShiftFactor { get; set; }

        public Color BackgroundColor => Color.FromName(BackgroundColorName);
        public Color FontColor => Color.FromName(FontColorName);
    }
}