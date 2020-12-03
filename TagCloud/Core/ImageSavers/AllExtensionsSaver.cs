using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Core.ImageSavers
{
    public class AllExtensionsSaver : IImageSaver
    {
        private readonly Dictionary<string, ImageFormat> formats;

        public AllExtensionsSaver()
        {
            formats = new Dictionary<string, ImageFormat>(
                StringComparer.CurrentCultureIgnoreCase)
            {
                ["jpeg"] = ImageFormat.Jpeg,
                ["jpg"] = ImageFormat.Jpeg,
                ["png"] = ImageFormat.Png,
                ["gif"] = ImageFormat.Gif,
                ["bmp"] = ImageFormat.Bmp,
                ["tif"] = ImageFormat.Tiff
            };
        }

        public string Save(Bitmap bitmap, string fullPath, string format)
        {
            if (!formats.ContainsKey(format))
                throw new ArgumentException(
                    $"Unsupported format {format}\nPlease use: {string.Join(", ", formats.Keys)}");

            var path = $"{fullPath}.{format}";
            bitmap.Save(path, formats[format]);
            return path;
        }
    }
}