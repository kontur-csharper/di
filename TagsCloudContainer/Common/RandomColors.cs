using System;
using System.Drawing;

namespace TagsCloudContainer.Common
{
    internal class RandomColors : IColorSettings
    {
        public Color BackgroundColor { get; set; } = Color.Black;

        public Color GetNextColor()
        {
            var rand = new Random(Environment.TickCount);
            return Color.FromArgb(rand.Next(30, 255), rand.Next(30, 255), rand.Next(30, 255));
        }
    }
}