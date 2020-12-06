using System;
using System.Drawing;

namespace WordCloudGenerator
{
    public static class ColorConverterExtensions
    {
        public static bool CanConvertFrom(this ColorConverter converter, object value)
        {
            if (value == null)
                return false;
            try
            {
                converter.ConvertFrom(value);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}