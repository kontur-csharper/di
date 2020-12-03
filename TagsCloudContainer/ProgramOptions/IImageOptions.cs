namespace TagsCloudContainer.ProgramOptions
{
    public interface IImageOptions
    {
        public int Width { get; set; }

        public int Height { get; set; }
        
        public string ImageOutputDirectory { get; set; }

        public string ImageName { get; set; }

        public string ImageExtension { get; set; }
    }
}