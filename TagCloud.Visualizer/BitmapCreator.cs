using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualizer
{
    public static class BitmapCreator
    {
        internal static Bitmap DrawBitmap(List<RectangleWithWord> rectanglesWithWords, ImageOptions opts)
        {
            var bitmap = new Bitmap(opts.ImageWidth, opts.ImageHeight);
            using var graph = Graphics.FromImage(bitmap);
            graph.Clear(Color.White);
            var brush = (Brush) typeof(Brushes).GetProperty($"{opts.ColorName}")?.GetValue(null);
            foreach (var rectangleWithWord in rectanglesWithWords)
            {
                using var font = new Font(opts.FontName, opts.FontSize * (float) rectangleWithWord.Word.Weight);
                graph.DrawString(rectangleWithWord.Word.Value,
                    font,
                    brush!,
                    rectangleWithWord.Rectangle);
            }

            return bitmap;
        }
    }
}