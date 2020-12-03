using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using TagCloudUI.Infrastructure;
using TagCloudUI.UI;

namespace TagCloudUI
{
    static class Program
    {
        public static void Main(string[] args)
        {
            var container = BuildContainer(args);
            var ui = container.Resolve<IUserInterface>();
            ui.Run(container.Resolve<IAppSettings>());
        }

        private static IContainer BuildContainer(IEnumerable<string> args)
        {
            var builder = new ContainerBuilder();

            var dlls = Directory.GetFiles(Environment.CurrentDirectory, "TagCloud*.dll");
            var assemblies = dlls.Select(Assembly.LoadFrom).ToArray();
            builder.RegisterAssemblyModules(assemblies);

            builder.RegisterInstance(AppSettings.Create(args)).AsSelf()
                .AsImplementedInterfaces().SingleInstance();

            return builder.Build();
        }
    }
}