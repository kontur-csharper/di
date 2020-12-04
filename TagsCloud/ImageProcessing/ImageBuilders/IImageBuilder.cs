using System.Collections.Generic;
using System.Drawing;
using TagsCloud.TagsCloudProcessing;

namespace TagsCloud.ImageProcessing.ImageBuilders
{
    public interface IImageBuilder
    {
        Bitmap BuildImage(IEnumerable<Tag> tags);
    }
}
