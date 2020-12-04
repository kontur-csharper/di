using System.Drawing;

namespace TagCloud.Settings
{
    public class CircularLayouterSettings
    {
        public Point Center { get; }
        public int SpiralPitch { get; }
        public double SpiralStep { get; }
        
        public CircularLayouterSettings(Point center, int spiralPitch, double spiralStep)
        {
            Center = center;
            SpiralPitch = spiralPitch;
            SpiralStep = spiralStep;
        }
    }
}