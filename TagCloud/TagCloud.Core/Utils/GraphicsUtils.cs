using System;
using System.Drawing;

namespace TagCloud.Core.Utils
{
    public static class GraphicsUtils
    {
        public static Image FitToSize(Image source, Size newSize)
        {
            var resizeCoefficient = Math.Min(
                (float) newSize.Height / source.Height,
                (float) newSize.Width / source.Width);
            var resized = new Bitmap(source, (source.Size * resizeCoefficient).ToSize());
            var location = new Point((newSize - resized.Size) / 2);

            var result = new Bitmap(newSize.Width, newSize.Height);
            using (var g = Graphics.FromImage(result))
                g.DrawImage(resized, location);
            return result;
        }

        public static Image PlaceAtCenter(Image image, Size newSize)
        {
            var newBitmap = new Bitmap(newSize.Width, newSize.Height);
            using var g = Graphics.FromImage(newBitmap);

            g.DrawImage(image, new Point((newSize - image.Size) / 2));
            return newBitmap;
        }

    }
}