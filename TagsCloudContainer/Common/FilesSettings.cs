namespace TagsCloudContainer.Common
{
    public class FilesSettings
    {
        public string DirectoryToSavePictures { get; set; } = "pictures";
        public string TextFilePath { get; set; } = @"..\..\text.txt";
        public string BoringWordsFilePath { get; set; } = @"..\..\boring words.txt";
        public string PictureFileName { get; set; } = "picture.png";
    }
}