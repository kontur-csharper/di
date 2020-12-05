using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public interface ITagsCloudDrawer
    {
        Bitmap Draw(Dictionary<string, int> countedWords);
    }
}