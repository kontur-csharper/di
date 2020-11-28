﻿using System.Drawing;

namespace TagCloud.Core.ImageSavers
{
    public interface IImageSaver
    {
        public string Save(Bitmap bitmap, string fullPath, string format);
    }
}