using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Drawer
{
    public class LayoutDrawer : ILayoutDrawer
    {
        private static readonly Regex ColorParser =
            new Regex(@"argb\((?<alpha>\d{1,3}),(?<red>\d{1,3}),(?<green>\d{1,3}),(?<blue>\d{1,3})\)");

        private readonly Random random;
        private readonly List<WordRectangle> rectangles;

        public LayoutDrawer()
        {
            random = new Random();
            rectangles = new List<WordRectangle>();
        }

        public void AddRectangle(WordRectangle rectangle)
        {
            rectangles.Add(rectangle);
        }

        public void Draw(Graphics graphics)
        {
            foreach (var rectangle in rectangles)
            {
                var brush = new SolidBrush(StringToArgbColor(rectangle.FontColor));
                using var arialFont = new Font(rectangle.FontFamily, rectangle.FontSize);
                graphics.DrawString(rectangle.Text, arialFont, brush, rectangle.Rectangle.Location);
            }
        }

        private Color StringToArgbColor(string s)
        {
            if (s == "random")
                return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            var match = ColorParser.Match(s).Groups;
            return Color.FromArgb(int.Parse(match["alpha"].Value), int.Parse(match["red"].Value),
                int.Parse(match["green"].Value), int.Parse(match["blue"].Value));
        }
    }
}