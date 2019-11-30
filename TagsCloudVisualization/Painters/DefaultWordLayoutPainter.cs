﻿using System;
using System.Drawing;
using TagsCloudVisualization.Core;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Painters
{
    public class DefaultWordLayoutPainter : WordLayoutPainter
    {
        public DefaultWordLayoutPainter(ImageSettings imageSettings, Font font, Palette palette) : base(imageSettings, font, palette)
        {}

        public override Bitmap GetDrawnLayoutedWords(LayoutedWord[] layoutedWords)
        {
            var bitmap = new Bitmap(ImageSettings.Width, ImageSettings.Height);
            var graphics = Graphics.FromImage(bitmap);
            // TODO: Draw words somehow
            throw new NotImplementedException();
        }
    }
}