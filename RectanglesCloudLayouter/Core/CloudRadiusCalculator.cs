using System.Drawing;
using System.Linq;
using RectanglesCloudLayouter.Interfaces;
using RectanglesCloudLayouter.SpecialMethods;

namespace RectanglesCloudLayouter.Core
{
    public class CloudRadiusCalculator : ICloudRadiusCalculator
    {
        public int CloudRadius { get; private set; }

        public void UpdateCloudRadius(Point spiralCenter, Rectangle currentRectangle)
        {
            var maxDistance = new[]
            {
                PointsDistance.GetCeilingDistanceBetweenPoints(currentRectangle.Location, spiralCenter),
                PointsDistance.GetCeilingDistanceBetweenPoints(
                    currentRectangle.Location + new Size(currentRectangle.Width, 0),
                    spiralCenter),
                PointsDistance.GetCeilingDistanceBetweenPoints(
                    currentRectangle.Location + new Size(0, currentRectangle.Height),
                    spiralCenter),
                PointsDistance.GetCeilingDistanceBetweenPoints(currentRectangle.Location + currentRectangle.Size,
                    spiralCenter)
            }.Max();
            if (maxDistance > CloudRadius)
                CloudRadius = maxDistance;
        }
    }
}