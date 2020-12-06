using System.Collections.Generic;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.TagCloudVisualizers
{
    public interface ICloudVisualizer
    {
        void PrintTagCloud(IEnumerable<Tag> tags);
    }
}