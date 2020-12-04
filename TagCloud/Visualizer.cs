using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloud.BackgroundPainter;

namespace TagCloud
{
    public class Visualizer: IVisualizer
    {
        private readonly ICanvas canvas;
        private readonly IPathCreater creater;
        private readonly ITagsCreater tagsCreater;
        private readonly IBackgroundPainter backgroundPainter;
        private const double fontCoefficient = 0.6;
        
        public Visualizer(ICanvas canvas, IPathCreater pathCreator, ITagsCreater tagsCreater, IBackgroundPainter backgroundPainter)
        {
            this.canvas = canvas;
            creater = pathCreator;
            this.tagsCreater = tagsCreater;
            this.backgroundPainter = backgroundPainter;
        }

        public string Visualize(string filename, FontFamily fontFamily, Color stringColor)
        {
            var bitmap = new Bitmap(canvas.Width, canvas.Height);
            var graphics = Graphics.FromImage(bitmap);
            var tags = tagsCreater.GetTags(filename, canvas.Height);
            
            backgroundPainter.Draw(tags, canvas, graphics);
            DrawAllStrings(tags, fontFamily, stringColor, graphics);
            
            var path = creater.GetNewPngPath();
            bitmap.Save(path);
            return path;
        }
        
        private void DrawAllStrings(List<Tuple<string, Rectangle>> tags, FontFamily fontFamily, Color color, Graphics graphics)
        {
            var textBrush = new SolidBrush(color);
            foreach (var (str, rectangle) in tags)
            {
                DrawString(str, rectangle, fontFamily, textBrush, graphics);
            }
        }

        private void DrawString(string str, Rectangle rectangle, FontFamily fontFamily, Brush textBrush, Graphics graphics)
        {
            var x = rectangle.X;
            var y = rectangle.Y;
            var fontSize = (int)Math.Round(rectangle.Height * fontCoefficient);
            if (rectangle.Height < 2)
                return;
            graphics.DrawString(str, new Font(fontFamily, fontSize), textBrush, x, y);
        }
    }
}