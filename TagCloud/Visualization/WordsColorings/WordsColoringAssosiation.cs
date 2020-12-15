using System.Drawing;
using System.Collections.Generic;
using System;

namespace TagCloud.Visualization.WordsColorings
{
    public static class WordsColoringAssosiation
    {
        private static readonly Color red = Color.FromArgb(255, 255, 0, 0);
        private static readonly Color green = Color.FromArgb(255, 0, 255, 0);
        private static readonly Color blue = Color.FromArgb(255, 0, 0, 255);
        private static readonly Color black = Color.FromArgb(255, 255, 255, 255);
        private static readonly Random random = new Random();
        private static readonly Dictionary<string, IWordsColoring> colorings =
            new Dictionary<string, IWordsColoring>
            {
                ["red"] = new WordsColoringConst(red),
                ["green"] = new WordsColoringConst(green),
                ["blue"] = new WordsColoringConst(blue),
                ["black"] = new WordsColoringConst(black),
                ["random"] = new WordsColoringConst(GetRandomColor()),
                ["multi"] = new WordsColoringRandom(),
                ["line red"] = new WordsColoringLineBringhtness(red),
                ["line green"] = new WordsColoringLineBringhtness(green),
                ["line blue"] = new WordsColoringLineBringhtness(blue),
                ["line random"] = new WordsColoringLineBringhtness(GetRandomColor())
            };

        private static Color GetRandomColor() => Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255));

        internal static IWordsColoring GetColoring(string name) => colorings.TryGetValue(name, out var coloring) ? coloring : null;
    }
}
