namespace TagsCloudVisualization.PointsGenerators
{
    public class SpiralParams
    {
        public int SpiralParameter { get; set; } 
        public float AngleStep { get; set; } 

        public SpiralParams(int spiralParameter = 2, float angleStep = 0.2f)
        {
            SpiralParameter = spiralParameter;
            AngleStep = angleStep;
        }
    }
}