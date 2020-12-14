using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface ICloudDrawer
    {
        Bitmap DrawCloud(IEnumerable<WordForCloud> wordsForCloud);
    }
}