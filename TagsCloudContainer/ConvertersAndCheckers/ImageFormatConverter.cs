using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.ConvertersAndCheckers
{
    public class ImageFormatConverter : IImageFormatConverter
    {
        private static Dictionary<string, ImageFormat> _imageFormats = new Dictionary<string, ImageFormat>
        {
            ["bmp"] = ImageFormat.Bmp,
            ["jpeg"] = ImageFormat.Jpeg,
            ["png"] = ImageFormat.Png,
            ["gif"] = ImageFormat.Gif,
            ["tiff"] = ImageFormat.Tiff
        };

        public ImageFormat ConvertToImageFormat(string imageFormatFromString)
        {
            if (_imageFormats.TryGetValue(imageFormatFromString.ToLower(), out var format))
                return format;
            throw new Exception("Doesn't contain this image format");
        }
    }
}