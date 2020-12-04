using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.ConsoleAppHelper
{
    public static class ColorsParser
    {
        public static Dictionary<char, Color> colors = new Dictionary<char, Color>
        {
            {'r', Color.Red},
            {'g', Color.Green},
            {'b', Color.Blue},
            {'y', Color.Yellow},
            {'p', Color.Purple}
        };

        public static Color[] ParseColors(string input)
        {
            var result = new List<Color>();
            foreach (var colorChar in input)
            {
                if (!colors.ContainsKey(colorChar))
                    throw new ArgumentException($"Color {colorChar} is not supported");
                result.Add(colors[colorChar]);
            }

            return result.ToArray();
        }
    }
}