using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public interface IFontFamilyProvider
    {
        FontFamily FontFamily { get; set; }
    }
}