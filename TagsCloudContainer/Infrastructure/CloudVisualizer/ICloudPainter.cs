using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.App.CloudGenerator;

namespace TagsCloudContainer.Infrastructure.CloudVisualizer
{
    internal interface ICloudPainter
    {
        public void Paint(IEnumerable<Tag> cloud, Graphics g);
    }
}