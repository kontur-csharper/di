using Cloud.ClientUI.ArgumentConverters;
using CloudContainer;
using Microsoft.Extensions.DependencyInjection;

namespace Cloud.ClientUI
{
    public class ContainerBuilder
    {
        public ServiceCollection CreateContainer()
        {
            var container = new ServiceCollection();
            container.AddSingleton<TagCloudContainerBuilder, TagCloudContainerBuilder>();
            container.AddSingleton<ISaver, PngSaver>();
            container.AddSingleton<IArgumentConverter, ArgumentConverter>();
            container.AddSingleton<TagCloudArguments, TagCloudArguments>();
            container.AddSingleton<TagCloudCreator, TagCloudCreator>();
            container.AddSingleton<TagCloudArgumentsCreator, TagCloudArgumentsCreator>();
            container.AddSingleton<IArgumentParser, ArgumentParser>();
            return container;
        }
    }
}