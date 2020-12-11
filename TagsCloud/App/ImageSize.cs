using System;

namespace TagsCloud.App
{
    public class ImageSize
    {
        private int height = 600;
        private int width = 600;

        public int Width
        {
            get => width;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("", $"{nameof(Width)} must be positive");
                width = value;
            }
        }

        public int Height
        {
            get => height;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("", $"{nameof(Height)} must be positive");
                height = value;
            }
        }
    }
}