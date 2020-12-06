using System;
using System.Collections.Generic;
using System.Drawing;
using RectanglesCloudLayouter.Core;

namespace RectanglesCloudLayouter.SpecialMethods
{
    public static class GenerateRectangles
    {
        public static IEnumerable<Rectangle> MakeLayouter(this CloudLayouter layouter,
            int countRectangles,
            int minRectangleWidth, int maxRectangleWidth,
            int minRectangleHeight, int maxRectangleHeight)
        {
            var random = new Random();
            for (var i = 0; i < countRectangles; i++)
            {
                yield return layouter.PutNextRectangle(new Size(random.Next(minRectangleWidth, maxRectangleWidth),
                    random.Next(minRectangleHeight, maxRectangleHeight)));
            }
        }
    }
}