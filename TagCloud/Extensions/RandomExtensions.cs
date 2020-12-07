using System;
using System.Drawing;

namespace TagCloud.Extensions
{
    public static class RandomExtensions
    {
        public static Size GetSize(this Random random, int minHeight, int maxHeight, double aspect)
        {
            var height = random.Next(minHeight, maxHeight);
            return new Size((int)(height * aspect), height);
        }


    }
}
