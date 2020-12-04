using System;
using System.Linq;
using TagsCloud.Factory;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;

namespace TagsCloud.ImageProcessing.SaverImage.Factory
{
    public class ImageSaverFactory : ServiceFactory<IImageSaver>
    {
        private readonly ImageConfig imageConfig;

        public ImageSaverFactory(ImageConfig imageConfig)
        {
            this.imageConfig = imageConfig;
        }

        public override IImageSaver Create()
        {
            var saver = services.FirstOrDefault(pair => pair.Value().CanSave(imageConfig.Path)).Value();
            if (saver == null)
                throw new InvalidOperationException("This file type is not supported");
            return saver;
        }
    }
}
