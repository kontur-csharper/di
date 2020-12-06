using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RectanglesCloudLayouter.Interfaces;
using RectanglesCloudLayouter.SpecialMethods;

namespace RectanglesCloudLayouter.Core
{
    public class CloudLayouter : ICloudLayouter
    {
        private readonly List<Rectangle> _rectangles;
        private readonly ISpiral _spiral;
        private readonly ICloudRadiusCalculator _cloudRadiusCalculator;

        public CloudLayouter(ISpiral spiral, ICloudRadiusCalculator cloudRadiusCalculator)
        {
            _rectangles = new List<Rectangle>();
            _spiral = spiral;
            _cloudRadiusCalculator = cloudRadiusCalculator;
        }

        public Rectangle GetCurrentRectangle => _rectangles.Last();

        public int RectanglesCount => _rectangles.Count;

        public int CloudRadius => _cloudRadiusCalculator.CloudRadius;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
                throw new ArgumentException("Size width and height must be positive");
            var rectangle = new Rectangle(_spiral.GetNewSpiralPoint() - rectangleSize / 2, rectangleSize);
            while (RectanglesIntersection.IsAnyIntersectWithRectangles(rectangle, _rectangles))
                rectangle = new Rectangle(_spiral.GetNewSpiralPoint() - rectangleSize / 2, rectangleSize);
            _rectangles.Add(rectangle);
            _cloudRadiusCalculator.UpdateCloudRadius(_spiral.Center, rectangle);
            return rectangle;
        }
    }
}