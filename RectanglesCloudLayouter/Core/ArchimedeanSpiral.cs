using System;
using System.Drawing;
using RectanglesCloudLayouter.Interfaces;

namespace RectanglesCloudLayouter.Core
{
    public class ArchimedeanSpiral : ISpiral
    {
        private double _spiralAngle;
        private ISpiralSettings _spiralSettings;
        public Point Center { get; }

        public ArchimedeanSpiral(Point center, ISpiralSettings spiralSettings)
        {
            Center = center;
            _spiralSettings = spiralSettings;
        }

        public Point GetNewSpiralPoint()
        {
            var position = new Point(
                (int) Math.Round(Center.X + _spiralSettings.SpiralStep * _spiralAngle * Math.Cos(_spiralAngle)),
                (int) Math.Round(Center.Y + _spiralSettings.SpiralStep * _spiralAngle * Math.Sin(_spiralAngle)));
            _spiralAngle += _spiralSettings.AdditionSpiralAngleFromDegrees * Math.PI / 180;
            return position;
        }
    }
}