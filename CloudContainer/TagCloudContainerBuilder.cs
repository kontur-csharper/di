using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.PointProviders;
using TagsCloudVisualization.WordsCleaners;
using TagsCloudVisualization.WordsConverters;
using TagsCloudVisualization.WordsProviders;

namespace CloudContainer
{
    public class TagCloudContainerBuilder
    {
        public ServiceCollection CreateTagCloudContainer(TagCloudArguments arguments)
        {
            var container = new ServiceCollection();
            container.AddSingleton<IWordProvider, TxtWordProvider>();
            container.AddSingleton<IPointProvider, PointProvider>();
            container.AddSingleton<ICloudLayout, CircularCloudLayouter>();
            container.AddSingleton<IConfig, Config>();
            container.AddSingleton<IWordConverter, WordsToCloudTagConverter>();
            container.AddSingleton<IWordsCleaner, BoringWordsCleaner>();
            container.AddSingleton<TagCloudContainer, TagCloudContainer>();
            container.AddSingleton(typeof(TagCloudArguments), arguments);
            return container;
        }
    }
}