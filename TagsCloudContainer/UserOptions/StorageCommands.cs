using MatthiWare.CommandLine.Core.Attributes;

namespace TagsCloudContainer.UserOptions
{
    public class StorageCommands
    {
        [Required, Name("i", "myText"),
         Description("The path to text file or file name with format in current directory")]
        public string PathToCustomText { get; set; }

        [Required, Name("o", "outImage"), Description("The path to save image without format or file name")]
        public string PathToSave { get; set; }

        [Name("e", "imageFormat"), DefaultValue("jpeg"),
         Description("Image format. Supported formats: \"bmp\", \"jpeg\", \"png\", \"gif\", \"tiff\"")]
        public string ImageFormat { get; set; }
    }
}