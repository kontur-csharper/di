using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainer.Interfaces
{
    public interface IVisualization
    {
        Image GetImageCloud(IReadOnlyList<WordTag> tags, int cloudRadius);
    }
}