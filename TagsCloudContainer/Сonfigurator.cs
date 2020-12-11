using System.Reflection;
using Autofac;

namespace TagsCloudContainer
{
    public static class Configurator
    {
        public static IContainer GetContainer()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyTypes(assembly).Except<StopWordsFilter>().AsImplementedInterfaces();
            containerBuilder.RegisterType<StopWords>().AsSelf();
            containerBuilder.RegisterType<StopWordsFilter>().AsSelf();
            containerBuilder.RegisterType<TagsCloudCreator>().AsSelf();
            containerBuilder.RegisterType<FixedColorProvider>().AsImplementedInterfaces();
            return containerBuilder.Build();
        }
    }
}
