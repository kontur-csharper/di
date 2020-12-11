using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public interface IImageSaver
    {
        HashSet<string> Extensions { get; }

        void Save(Image image, string fileName);
    }
}