using System;
using System.Drawing;

namespace TagCloud_Tests.Layouter
{
    public class SizesGenerator
    {
        public static Size[] GenerateSizes(int count, Size minSize, Size maxSize,
            int? seed = null)
        {
            if (minSize.Height >= maxSize.Height || minSize.Width >= maxSize.Width)
                throw new ArgumentException("minSizes fields must be less than maxSizes fields");
            
            if (count <= 0)
                throw new ArgumentException("Count must be greater than 0");

            Random random;
            random = seed != null ? new Random((int)seed) : new Random();
            
            var sizes = new Size[count];
            for (var i = 0; i < count; i++)
            {
                var newSize = new Size(
                    random.Next(minSize.Width, maxSize.Width),
                    random.Next(minSize.Height, maxSize.Height));
                sizes[i] = newSize;
            }

            return sizes;
        }
    }
}