using RectanglesCloudLayouter.Interfaces;

namespace RectanglesCloudLayouter.Core
{
    public class SpiralSettings : ISpiralSettings
    {
        public double AdditionSpiralAngleFromDegrees { get; }
        public double SpiralStep { get; }

        public SpiralSettings(double additionSpiralAngleFromDegrees, double spiralStep)
        {
            AdditionSpiralAngleFromDegrees = additionSpiralAngleFromDegrees;
            SpiralStep = spiralStep;
        }
    }
}