using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Drawers
{
    public interface ITagDrawer
    {
        public Bitmap DrawTagCloud(IReadOnlyCollection<TagInfo> tags);
    }
}