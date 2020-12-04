using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ITagsCreater
    {
        List<Tuple<String, Rectangle>> GetTags(string filename, int canvasHeight);
    }
}