using System.Drawing;

namespace HomeExercise.settings
{
    public class SpiralSettings
    {
        public Point Center { get; }
        public Point? PreviousPoint { get; }
        public float Step { get; }
        public float Angle { get; }
        
        public SpiralSettings(Point center, float step = 0.0005f, float angle  = 0f, Point? previousPoint = null)
        {
            Center = center;
            Step = step;
            Angle = angle;
            PreviousPoint = previousPoint;
        }
    }
}