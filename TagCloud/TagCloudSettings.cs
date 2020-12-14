using System.Drawing;

namespace TagCloud
{
    public class TagCloudSettings
    {
        public Size PictureSize { get; }
        public Point CloudCenter { get; }
        public Color[] Colors { get; }
        public string FontName { get; }
        public int MaxFontSize { get; }
        public string InputFile { get; }
        public string BoringWordsFile { get; }
        public string OutputFile { get; }

        public TagCloudSettings(Size pictureSize,
                                Point cloudCenter, Color[] colors, string fontName,
                                int maxFontSize, string inputFile, string boringWordsFile, string outputFile)
        {
            PictureSize = pictureSize;
            CloudCenter = cloudCenter;
            Colors = colors;
            FontName = fontName;
            MaxFontSize = maxFontSize;
            InputFile = inputFile;
            BoringWordsFile = boringWordsFile;
            OutputFile = outputFile;
        }
    }
}