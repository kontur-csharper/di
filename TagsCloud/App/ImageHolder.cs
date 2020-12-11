using System.Drawing;
using System.IO;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class ImageHolder : IImageHolder
    {
        private readonly ImageSaverProvider imageSaverProvider;
        private Image image;

        public ImageHolder(ImageSaverProvider imageSaverProvider)
        {
            this.imageSaverProvider = imageSaverProvider;
        }

        public Graphics StartDrawing() => Graphics.FromImage(image);

        public void RecreateImage(ImageSize imageSize) => image = new Bitmap(imageSize.Width, imageSize.Height);

        public Result<None> SaveImage(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (image == null)
                return Result.Fail<None>("Tag cloud not created. First create tag cloud by command 'tagcloud'");
            if (extension == string.Empty)
                return Result.Fail<None>("Please write extension of file");
            return imageSaverProvider.GetImageSaver(extension)
                .Then(x => x.Save(image, fileName));
        }
    }
}