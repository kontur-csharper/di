using System;
using System.Drawing;

namespace TagCloud.Visualization.WordsColorings
{
    internal class WordsColoringLineBringhtness : IWordsColoring
    {
        private readonly Color color;

        internal WordsColoringLineBringhtness(Color color) => this.color = color;
        public Color GetColor(string word, Rectangle location, TagCloud cloud)
        {
            var center = new Point(
                (location.Left + location.Right) / 2, 
                (location.Top + location.Bottom) / 2);
            var length = Length(center, cloud.Center);
            var farPoint = new Point(cloud.Right, cloud.Bottom);
            var farLength = Length(farPoint, cloud.Center);
            var bringhtness = -(255 / farLength) * length + 255;
            return Color.FromArgb((int)bringhtness, color.R, color.G, color.B);
        }

        private static double Length(Point p1, Point p2) =>
            Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
    }
}
