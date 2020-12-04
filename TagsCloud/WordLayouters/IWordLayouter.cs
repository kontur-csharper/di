using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.WordLayouters
{
    public interface IWordLayouter
    {
        void AddWords(Dictionary<string, int> statistic);
        List<CloudWord> CloudWords { get; }
        Rectangle GetCloudRectangle();
    }
}