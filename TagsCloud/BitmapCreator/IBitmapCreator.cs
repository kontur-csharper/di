using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.BitmapCreator
{
    public interface IBitmapCreator : IDisposable
    {
        public Bitmap Create(IReadOnlyCollection<string> words);
    }
}
