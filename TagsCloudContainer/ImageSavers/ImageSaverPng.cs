using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagsCloudContainer
{
    class ImageSaverPng : ImageSaver
    {
        public ImageSaverPng()
        {
            FormatName = "png";
            Format = ImageFormat.Png;
        }
    }
}
