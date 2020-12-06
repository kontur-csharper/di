using CloudContainer;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.ClientUI
{
    public class TagCloudCreator
    {
        private readonly TagCloudContainerBuilder container;
        private readonly ISaver saver;

        public TagCloudCreator(TagCloudContainerBuilder container, ISaver saver)
        {
            this.container = container;
            this.saver = saver;
        }

        public void Run(TagCloudArguments arguments)
        {
            var visualizationContainer = container.CreateTagCloudContainer(arguments);
            var image = visualizationContainer.BuildServiceProvider().GetService<TagCloudContainer>()
                .GetImage(arguments);
            saver.SaveImage(image, arguments.OutputFileName);
        }
    }
}