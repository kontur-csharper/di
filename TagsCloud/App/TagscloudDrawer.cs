﻿using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    class TagscloudDrawer : ITagscloudDrawer
    {
        private IRectanglesConstellator constellator;

        public TagscloudDrawer(IRectanglesConstellator constellator)
        {
            this.constellator = constellator;
        }

        public Image GetTagscloud(Dictionary<string, int> words, TagscloudSettings settings, double cloudToImageScaleRatio)
        {
            if (cloudToImageScaleRatio <= 0 || cloudToImageScaleRatio > 1)
                throw new ArgumentException("ratio should be positive and be less 1");
            var tagscloudWords = new List<TagscloudWord>();
            foreach (var word in words)
            {
                var wordFont = new Font(settings.CurrentFontFamily, word.Value * 10, settings.CurrentFontStyle);
                tagscloudWords.Add(new TagscloudWord(
                    word.Key, wordFont, 
                    constellator.PutNextRectangle(
                        ConvertWordToSize(word.Key, wordFont)).Location));
            }
            var newSize = new ImageSize((int)(settings.ImageSize.Height * cloudToImageScaleRatio), 
                (int)(settings.ImageSize.Width * cloudToImageScaleRatio));
            var newRatio = CalculateRatio(constellator, newSize);
            for (var i = 0; i < tagscloudWords.Count; i++)
            {
                tagscloudWords[i] = new TagscloudWord(tagscloudWords[i].Value, 
                    new Font(tagscloudWords[i].Font.FontFamily, (int)(tagscloudWords[i].Font.Size * newRatio)),
                    new Point((int)(tagscloudWords[i].Position.X * newRatio), (int)(tagscloudWords[i].Position.Y * newRatio)));
            }
            constellator.Clear();
            return DrawTagscloud(tagscloudWords, settings);
        }

        public void SetNewConstellator(IRectanglesConstellator newConstellator)
        {
            constellator = newConstellator;
        }

        private static double CalculateRatio(IRectanglesConstellator constellator, ImageSize newSize)
        {
            double newRatio;
            if ((double) constellator.Width / newSize.Width > (double) constellator.Height / newSize.Height)
                newRatio = (double) newSize.Width / constellator.Width;
            else newRatio = (double) newSize.Height / constellator.Height;
            return newRatio;
        }

        private Image DrawTagscloud(IEnumerable<TagscloudWord> words, TagscloudSettings settings)
        {
            var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var graphics = Graphics.FromImage(image);
            graphics.TranslateTransform((float)image.Width / 2, (float)image.Height / 2);
            graphics.Clear(settings.Palette.BackgroundColor);
            foreach (var word in words)
            {
                graphics.DrawString(word.Value, word.Font, 
                    new SolidBrush(settings.Palette.PrimaryColor), word.Position);
            }
            return image;
        }

        private Size ConvertWordToSize(string word, Font font)
        {
            var floatSize = Graphics.FromImage(new Bitmap(10, 10)).MeasureString(word, font);
            return new Size((int)Math.Ceiling((decimal)floatSize.Width), (int)Math.Ceiling((decimal)floatSize.Height));
        }
    }
}
