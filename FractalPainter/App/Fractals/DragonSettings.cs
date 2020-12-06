using System;

namespace FractalPainting.App.Fractals
{
    public class DragonSettings
    {
        public double Angle1 { get; set; } = Math.PI / 4;
        public double Angle2 { get; set; } = 3 * Math.PI / 4;
        public float ShiftX { get; set; } = 1;
        public float ShiftY { get; set; } = 0;
        public float Scale { get; set; } = (float) (1 / Math.Sqrt(2));
        public int IterationsCount { get; set; } = 20000;
    }
}