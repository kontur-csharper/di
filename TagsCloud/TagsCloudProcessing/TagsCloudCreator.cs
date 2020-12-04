using TagsCloud.Factory;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;
using TagsCloud.TagsCloudProcessing.TagsGenerators;
using TagsCloud.TextProcessing;

namespace TagsCloud.TagsCloudProcessing
{
    public class TagsCloudCreator
    {
        private readonly TextProcessor textProcessor;
        private readonly IImageBuilder imageBuilder;
        private readonly IServiceFactory<IImageSaver> imageSaverFactory;
        private readonly IServiceFactory<ITagsGenerator> tagsGeneratorFactory;

        public TagsCloudCreator(TextProcessor textProcessor,
             IImageBuilder imageBuilder, IServiceFactory<IImageSaver> imageSaverFactory,
             IServiceFactory<ITagsGenerator> tagsGeneratorFactory)
        {
            this.tagsGeneratorFactory = tagsGeneratorFactory;
            this.textProcessor = textProcessor;
            this.imageSaverFactory = imageSaverFactory;
            this.imageBuilder = imageBuilder;
        }

        public void CreateCloud(string textPath, string imageSavePath)
        {
            var words = textProcessor.ReadFromFile(textPath);
            var tags = tagsGeneratorFactory.Create().CreateTags(words);

            using var image = imageBuilder.BuildImage(tags);
            imageSaverFactory.Create().SaveImage(image, imageSavePath);
        }
    }
}
