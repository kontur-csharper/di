namespace TagsCloud.Visualization
{
    public class ImageSettings
    {
        private int width = 800;
        private int height = 800;
        private const int MinWidth = 300;
        private const int MaxWidth = 1920;
        private const int MinHeight = 300;
        private const int MaxHeight = 1080;

        public int Width
        {
            get => width;
            set
            {
                if (value > MaxWidth)
                    width = MaxWidth;
                else if (value < MinWidth)
                    width = MinWidth;
                else width = value;
            }
        }

        public int Height
        {
            get => height;
            set
            {
                if (value > MaxHeight)
                    height = MaxHeight;
                else if (value < MinHeight)
                    height = MinHeight;
                else height = value;
            }
        }
    }
}
