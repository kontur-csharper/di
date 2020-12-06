using System;
using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.ConvertersAndCheckers
{
    public class FontConverter : IFontConverter
    {
        public Font ConvertToFont(string[] fontParameters)
        {
            if (fontParameters.Length != 2)
                throw new Exception("Invalid number parameters of font");
            if (!float.TryParse(fontParameters[1], out var fontSize) ||
                !TryConvertToFont(fontParameters[0], fontSize, out var font))
                throw new Exception("Invalid parameters of font");
            return font;
        }

        private static bool TryConvertToFont(string fontName, float fontSize, out Font font)
        {
            try
            {
                font = new Font(fontName, fontSize);
                return true;
            }
            catch (Exception)
            {
                font = default;
                return false;
            }
        }
    }
}