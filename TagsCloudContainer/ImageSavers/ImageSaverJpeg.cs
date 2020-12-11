using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagsCloudContainer
{
    class ImageSaverJpeg : ImageSaver
    {
        public ImageSaverJpeg()
        {
            FormatName = "jpeg";
            Format = ImageFormat.Jpeg;
        }
    }
}
