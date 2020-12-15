using System.Drawing;
using System;
using System.Linq;
using System.Collections.Generic;

namespace TagCloud.Visualization
{
    internal static class Fonts
    {
        private static readonly HashSet<string> fonts = 
            FontFamily
            .Families
            .Select(f => f.Name)
            .ToHashSet();
        internal static string GetRandomFont() => 
            FontFamily.Families[new Random().Next(fonts.Count)].Name;

        internal static string GetFont(string font) => fonts.Contains(font) ? font : GetRandomFont();
    }
}
