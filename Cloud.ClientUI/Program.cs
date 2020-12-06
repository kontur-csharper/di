using Microsoft.Extensions.DependencyInjection;

namespace Cloud.ClientUI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            var container = containerBuilder.CreateContainer().BuildServiceProvider();
            var arguments = container.GetService<TagCloudArgumentsCreator>().GetArguments(args);
            container.GetService<TagCloudCreator>().Run(arguments);
        }
    }
}