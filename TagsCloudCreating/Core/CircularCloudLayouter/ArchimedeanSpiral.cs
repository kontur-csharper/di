using System;
using System.Drawing;
using TagsCloudCreating.Configuration;

namespace TagsCloudCreating.Core.CircularCloudLayouter
{
    public class ArchimedeanSpiral
    {
        private Point Center { get; }
        private double Angle { get; }
        private double Step { get; }
        private double polarArgument;

        public ArchimedeanSpiral(CloudLayouterSettings layouterSettings)
        {
            Center = layouterSettings.StartPoint;
            Angle = layouterSettings.PolarAngle;
            Step = layouterSettings.Step;
            polarArgument = 0;
        }

        public Point GetNextPoint()
        {
            var radius = polarArgument * Angle;
            var x = radius * Math.Cos(polarArgument);
            var y = radius * Math.Sin(polarArgument);
            polarArgument += Step;
            return new Point(Center.X + (int) Math.Round(x), Center.Y - (int) Math.Round(y));
        }
    }
}