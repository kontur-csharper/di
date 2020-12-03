namespace TagsCloudContainer.ProgramOptions
{
    public class FontOptions : IFontOptions
    {
        public string FontFamily { get; set; }
        public string FontColor { get; set; }

        public FontOptions(string fontFamily, string fontColor)
        {
            FontFamily = fontFamily;
            FontColor = fontColor;
        }
    }
}