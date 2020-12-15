using System;
using System.ComponentModel;
using System.Drawing;

namespace TagsCloudCreating.Configuration
{
    public class CloudLayouterSettings
    {
        [DisplayName("Start point")] public Point StartPoint { get; set; } = new Point(1500, 1500);

        [DisplayName("Polar angle for Archimedean spiral")]
        public double PolarAngle { get; set; } = Math.PI / 6;

        [DisplayName("Step to next point")] public double Step { get; set; } = 0.01;

        [DisplayName("Need to compact the cloud?")]
        public bool NeedingShiftToCenter { get; set; } = true;
    }
}