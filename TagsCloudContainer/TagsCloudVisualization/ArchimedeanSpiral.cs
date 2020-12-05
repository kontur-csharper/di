using System;
using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization.Interfaces;

namespace TagsCloudContainer.TagsCloudVisualization
{
    public class ArchimedeanSpiral : ISpiral
    {
        private readonly double angleDelta;
        private readonly double distanceBetweenLoops;
        private double angle;

        public ArchimedeanSpiral(Point center, double distanceBetweenLoops, double angleDelta)
        {
            angle = 0;
            Center = center;
            this.angleDelta = angleDelta;
            this.distanceBetweenLoops = distanceBetweenLoops;
            Type = SpiralType.Archimedean;
            ValidateSpiralParameters();
        }

        public SpiralType Type { get; }

        public Point Center { get; }

        public Point GetNextPoint()
        {
            var x = Center.X + (int) (distanceBetweenLoops * angle * Math.Cos(angle));
            var y = Center.Y + (int) (distanceBetweenLoops * angle * Math.Sin(angle));
            angle += angleDelta;

            return new Point(x, y);
        }

        private void ValidateSpiralParameters()
        {
            if (Center.X < 0 || Center.Y < 0)
                throw new ArgumentException("Center coordinates should not be negative numbers");

            if (angleDelta <= 0)
                throw new ArgumentException("angleDelta should not be negative or zero");

            if (distanceBetweenLoops <= 0)
                throw new ArgumentException("distanceBetweenLoops should not be negative or zero");
        }
    }
}