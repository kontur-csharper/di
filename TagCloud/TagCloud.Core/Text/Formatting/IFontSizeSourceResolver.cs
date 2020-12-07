namespace TagCloud.Core.Text.Formatting
{
    public interface IFontSizeSourceResolver
    {
        IFontSizeSource Get(FontSizeSourceType type);
    }
}