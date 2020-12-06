using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface IFontConverter
    {
        Font ConvertToFont(string[] fontParameters);
    }
}