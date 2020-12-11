using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface IFontSizeCalculator
    {
        IEnumerable<WordWithFont> CalculateFontSize(IEnumerable<string> words, FontFamily fontFamily);
    }
}