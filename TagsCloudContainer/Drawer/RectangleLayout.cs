using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.ProgramOptions;

namespace TagsCloudContainer.Drawer
{
    public class RectangleLayout : IRectangleLayout
    {
        private readonly ILayouter layouter;
        private readonly ILayoutDrawer drawer;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly IImageOptions imageOptions;
        private readonly IFontOptions fontOptions;

        public RectangleLayout(ILayouter layouter, ILayoutDrawer drawer, IImageOptions imageOptions,
            IFontOptions fontOptions)
        {
            this.layouter = layouter;
            this.drawer = drawer;
            this.imageOptions = imageOptions;
            this.fontOptions = fontOptions;
            bitmap = new Bitmap(imageOptions.Width, imageOptions.Height);
            graphics = Graphics.FromImage(bitmap);
        }

        public void PlaceWords(Dictionary<string, int> words)
        {
            layouter.SetCenter(new Point(imageOptions.Width / 2, imageOptions.Height / 2));
            foreach (var (word, count) in words)
            {
                var fontSize = CalculateFontSize(count);
                var rectangle = layouter.PutNextRectangle(GetWordSize(word, fontSize, fontOptions.FontFamily));
                drawer.AddRectangle(new WordRectangle(rectangle, word, fontSize, fontOptions.FontFamily,
                    fontOptions.FontColor));
            }
        }

        private Size GetWordSize(string word, int fontSize, string fontFamily)
        {
            return graphics.MeasureString(word, new Font(fontFamily, fontSize)).ToSize();
        }

        private static int CalculateFontSize(int wordCount)
        {
            return (int) (Math.Log(Math.Max(wordCount, 6), 6) * 10);
        }

        public void DrawLayout()
        {
            drawer.Draw(graphics);
        }

        public void SaveLayout()
        {
            var outputDirectory = imageOptions.ImageOutputDirectory ?? Directory.GetCurrentDirectory();
            var fullPath = Path.Combine(outputDirectory, imageOptions.ImageName + imageOptions.ImageExtension);
            bitmap.Save(fullPath);
            Console.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }
    }
}