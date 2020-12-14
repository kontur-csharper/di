using System.Drawing;
using TagsCloudVisualization;

namespace TagCloud.Interfaces
{
    public interface IPointsFactory
    {
        IPoints Get(Point cloudCenter);
    }
}