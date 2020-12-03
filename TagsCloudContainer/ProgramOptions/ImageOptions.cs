namespace TagsCloudContainer.ProgramOptions
{
    public class ImageOptions : IImageOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string ImageOutputDirectory { get; set; }
        public string ImageName { get; set; }
        public string ImageExtension { get; set; }

        public ImageOptions(int width, int height, string imageOutputDirectory, string imageName, string imageExtension)
        {
            Width = width;
            Height = height;
            ImageOutputDirectory = imageOutputDirectory;
            ImageName = imageName;
            ImageExtension = imageExtension;
        }
    }
}