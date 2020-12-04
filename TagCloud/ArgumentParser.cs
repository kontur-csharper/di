using System;
using System.Drawing;
using System.IO;

namespace TagCloud
{
    public class ArgumentParser
    {
        private static IPathCreater pathCreater = new PathCreater();
        
        ///<exception cref="ArgumentException">Unknown background type</exception>
        public static Background GetBackground(string background)
        {
            switch (background)
            {
                case "empty":
                    return Background.Empty;
                case "circle":
                    return Background.Circle;
                case "rectangles":
                    return Background.Rectangles;
                default:
                    throw new ArgumentException("Unknown background type");
            }
        }
        
        ///<exception cref="ArgumentException">Incorrect size argument</exception>
        public static Size GetSize(string size)
        {
            var arr = size.Split(',');
            if (arr.Length == 2 && int.TryParse(arr[0], out var width) && int.TryParse(arr[1], out var height))
            {
                return new Size(width, height);
            }
            
            throw new ArgumentException("Incorrect size argument");
        }

        ///<exception cref="ArgumentException">Input file not found</exception>
        public static string CheckFileName(string fileName)
        {
            if (!File.Exists(pathCreater.GetPathToFile(fileName)))
            {
                throw new ArgumentException("Input file not found");
            }
            if (!fileName.EndsWith(".txt") && !fileName.EndsWith(".docx"))
            {
                throw new ArgumentException(@"Not supported format. Expected: .txt\.docx");
            }
            return fileName;
        }

        ///<exception cref="ArgumentException">Unknown FontFamily</exception>
        public static FontFamily GetFont(string font)
        {
            try
            {
                return new FontFamily(font);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("Unknown FontFamily");
            }
        }
        
        ///<exception cref="ArgumentException">Incorrect color format</exception>
        public static Color ParseColor(string colorInRGB)
        {
            var arr = colorInRGB.Split(',');
            if (arr.Length == 3
                && TryGetColorComponent(arr[0], out var red)
                && TryGetColorComponent(arr[1], out var green)
                && TryGetColorComponent(arr[1], out var blue))
            {
                return Color.FromArgb(red, green, blue);
            }
            throw new ArgumentException($"Incorrect color format. Given: {colorInRGB}. Expected: 0-255,0-255,0-255");
        }
        
        private static bool TryGetColorComponent(string colorComponent, out int value)
        {
            if (int.TryParse(colorComponent, out var intColorComponent))
            {
                value = intColorComponent;
                return intColorComponent >= 0 && intColorComponent <= 255;
            }

            value = 0;
            return false;
        }
    }
}