using System.Collections.Generic;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class ImageSaverProvider
    {
        private readonly IEnumerable<IImageSaver> imageSavers;

        public ImageSaverProvider(IEnumerable<IImageSaver> imageSavers)
        {
            this.imageSavers = imageSavers;
        }

        public Result<IImageSaver> GetImageSaver(string extension) => 
            imageSavers.FirstOrDefault(x => x.Extensions.Contains(extension))?.AsResult() 
            ?? Result.Fail<IImageSaver>("Can't save image in this format");
    }
}