namespace TagsCloudContainer.Common
{
    public class ColorSettingsProvider
    {
        public IColorSettings ColorSettings { get; set; } = new Palette();
    }
}