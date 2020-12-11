using System;
using System.IO;
using System.Reflection;
using Autofac;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    internal class Program
    {
        private static void Main(string[] args) => GetContainer().Resolve<IClient>().Run();

        private static IContainer GetContainer()
        {
            var dataAccess = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(x => x.GetInterfaces().Length != 0)
                .Except<ImageSettings>(
                    x => x
                        .AsSelf()
                        .As<IFontFamilyProvider, IImageColorProvider, IImageSizeProvider>()
                        .SingleInstance())
                .Except<ImageHolder>(x => x
                    .As<IImageHolder>()
                    .SingleInstance())
                .AsImplementedInterfaces();
            builder.RegisterInstance(Console.Out).As<TextWriter>();
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(x => x.GetInterfaces().Length == 0)
                .Except<None>();
            return builder.Build();
        }
    }
}