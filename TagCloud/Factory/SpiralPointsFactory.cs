using System.Drawing;
using TagCloud.Interfaces;
using TagsCloudVisualization;

namespace TagCloud.Factory
{
    public class SpiralPointsFactory : IPointsFactory
    {
        public IPoints Get(Point cloudCenter) =>
            new SpiralPoints(cloudCenter);
    }
}