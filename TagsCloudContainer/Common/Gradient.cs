using System;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.TextAnalyzing;

namespace TagsCloudContainer.Common
{
    internal class Gradient : IColorSettings
    {
        private readonly TagCreator tagCreator;
        private Color currentColor;
        private int deltaB;
        private int deltaG;
        private int deltaR;
        private int numberOfColor;
        private int tagsCount;

        public Gradient(TagCreator tagCreator)
        {
            this.tagCreator = tagCreator;
        }

        public Color ColorFrom { get; set; } = Color.White;
        public Color ColorTo { get; set; } = Color.Blue;
        public Color BackgroundColor { get; set; } = Color.Black;

        public Color GetNextColor()
        {
            if (numberOfColor == 0)
                RenewGradient();
            var currentR = GetNumberOfColor(currentColor.R + deltaR);
            var currentG = GetNumberOfColor(currentColor.G + deltaG);
            var currentB = GetNumberOfColor(currentColor.B + deltaB);
            currentColor = Color.FromArgb(currentR, currentG, currentB);
            numberOfColor++;
            if (numberOfColor == tagsCount)
                numberOfColor = 0;
            return currentColor;
        }

        public void RenewGradient()
        {
            tagsCount = tagCreator.GetTagsForVisualization().Count();
            deltaR = GetDelta(ColorTo.R, ColorFrom.R);
            deltaG = GetDelta(ColorTo.G, ColorFrom.G);
            deltaB = GetDelta(ColorTo.B, ColorFrom.B);
            currentColor = ColorFrom;
        }

        private int GetNumberOfColor(int calculatedNumber)
        {
            return Math.Min(Math.Max(calculatedNumber, 0), 255);
        }

        private int GetDelta(int colorTo, int colorFrom)
        {
            var delta = (double) (colorTo - colorFrom) / tagsCount;
            return (int) (delta > 0 ? Math.Max(delta, 1) : delta < 0 ? Math.Min(delta, -1) : 0);
        }
    }
}