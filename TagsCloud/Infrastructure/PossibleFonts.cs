using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class PossibleFonts
    {
        public PossibleFonts(HashSet<FontStyle> fontStyles, HashSet<FontFamily> fontFamilies)
        {
            if (fontStyles.Count == 0 || fontFamilies.Count == 0)
                throw new ArgumentException("There's not enough settings for font");
            FontStyles = fontStyles;
            FontFamilies = fontFamilies;
        }

        public HashSet<FontFamily> FontFamilies { get; set; }
        public HashSet<FontStyle> FontStyles { get; set; }
    }
}