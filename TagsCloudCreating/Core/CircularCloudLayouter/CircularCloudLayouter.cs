using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Contracts;
using TagsCloudLayouters.Infrastructure;

namespace TagsCloudCreating.Core.CircularCloudLayouter
{
    public class CircularCloudLayouter : ITagsCloudLayouter
    {
        public CloudLayouterSettings LayouterSettings { get; }
        private List<Rectangle> Rectangles { get; }
        private ArchimedeanSpiral Spiral { get; set; }
        private bool NeedingShiftToCenter { get; set; }
        private Point Center { get; set; }

        public CircularCloudLayouter(CloudLayouterSettings layouterSettings)
        {
            LayouterSettings = layouterSettings;
            Center = layouterSettings.StartPoint;
            NeedingShiftToCenter = layouterSettings.NeedingShiftToCenter;
            Spiral = new ArchimedeanSpiral(layouterSettings);
            Rectangles = new List<Rectangle>();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            Rectangle nextRectangle;

            do nextRectangle = new Rectangle(Spiral.GetNextPoint(), rectangleSize);
            while (Rectangles.Any(r => r.IntersectsWith(nextRectangle)));

            if (NeedingShiftToCenter)
                nextRectangle = GetShiftedToCenterRectangle(nextRectangle);
            Rectangles.Add(nextRectangle);
            return nextRectangle;
        }

        public void Recreate()
        {
            Center = LayouterSettings.StartPoint;
            NeedingShiftToCenter = LayouterSettings.NeedingShiftToCenter;
            Spiral = new ArchimedeanSpiral(LayouterSettings);
            Rectangles.Clear();
        }

        private Rectangle GetShiftedToCenterRectangle(Rectangle initialRectangle)
        {
            var minDistanceToCenter = double.MaxValue;
            var shiftedRectangle = initialRectangle;
            var queue = new Queue<Rectangle>();
            queue.Enqueue(shiftedRectangle);

            while (queue.Any())
            {
                var currentRectangle = queue.Dequeue();
                var distanceToCenter = currentRectangle.Location.DistanceTo(Center);
                if (Rectangles.Any(r => r.IntersectsWith(currentRectangle)) ||
                    distanceToCenter >= minDistanceToCenter)
                    continue;
                minDistanceToCenter = distanceToCenter;
                shiftedRectangle = currentRectangle;
                GetNeighboursFor(currentRectangle).ForEach(r => queue.Enqueue(r));
            }

            return shiftedRectangle;
        }


        private static List<Rectangle> GetNeighboursFor(Rectangle rectangle)
        {
            var neighbours = new (int X, int Y)[] {(1, 0), (0, 1), (-1, 0), (0, -1)};
            return neighbours
                .Select(p => new Rectangle(p.X + rectangle.X, p.Y + rectangle.Y, rectangle.Width, rectangle.Height))
                .ToList();
        }
    }
}