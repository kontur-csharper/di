﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public class CloudTagDrawer
    {
        private readonly IWordsAnalyzer wordsAnalyzer;
        private readonly ITagMaker tagMaker;
        private readonly int height;
        private readonly int width;
        private readonly string outputFilename;

        public CloudTagDrawer(
            IWordsAnalyzer wordsAnalyzer,
            ITagMaker tagMaker,
            int height,
            int width,
            string outputFilename
        )
        {
            this.wordsAnalyzer = wordsAnalyzer;
            this.tagMaker = tagMaker;
            this.height = height;
            this.width = width;
            this.outputFilename = outputFilename;
        }

        public void DrawTagsToFile()
        {
            var frequencyDict = wordsAnalyzer.GetWordsFrequensy();
            var tagRectangles = tagMaker.MakeTagRectangles(frequencyDict);
            
            var bitmap = DrawTagsOnBitmap(tagRectangles);
            bitmap.Save(outputFilename);
        }

        public void DrawTagsToForm()
        {
            var frequencyDict = wordsAnalyzer.GetWordsFrequensy();
            var tagRectangles = tagMaker.MakeTagRectangles(frequencyDict);
            
            var bitmap = DrawTagsOnBitmap(tagRectangles);
            bitmap.ToForm();
        }


        private Bitmap DrawTagsOnBitmap(Dictionary<Rectangle, (string, Font)>tagRectangles)
        {
            var bitmap = new Bitmap(width, height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                var selPen = new Pen(Color.Blue);
                var selBrush = new SolidBrush(Color.Black);
                foreach (var tag in tagRectangles)
                {
                    g.DrawString(tag.Value.Item1, tag.Value.Item2, selBrush, tag.Key.X, tag.Key.Y);
                }
            }
            return bitmap;
        }
    }
    internal static class BitmapExtensions
    {
        public static void ToForm(this Bitmap bitmap)
        {
            Form aForm = new Form();
            aForm.Width = bitmap.Width;
            aForm.Height = bitmap.Height;
            aForm.BackgroundImage = bitmap;
            aForm.ShowDialog();
        } 
    }
}