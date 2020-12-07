using System;
using System.Reflection;
using System.Windows.Forms;
using Autofac;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.CloudLayouters;
using TagsCloud.Core;
using TagsCloud.FileReader;
using TagsCloud.Spirals;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI
{
    internal static class Program
    { 
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(BuildContainer().Resolve<MainForm>());
        }

        private static IContainer BuildContainer()
        {
            var service = new ContainerBuilder();
            service.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            service.RegisterType<SpiralSettings>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<FontSetting>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<PathSettings>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<Palette>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<ColorAlgorithm>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<ImageSettings>().AsSelf().InstancePerLifetimeScope();
            service.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>()
                .InstancePerLifetimeScope();

            service.RegisterType<SpiralFactory>().AsImplementedInterfaces();
            service.RegisterType<CloudLayouterFactory>().AsImplementedInterfaces();
            service.RegisterType<ReaderFactory>().AsImplementedInterfaces();

            service.RegisterType<CloudVisualization>().InstancePerLifetimeScope();
            service.RegisterType<TagsCloudPainter>().InstancePerLifetimeScope();

            service.RegisterType<MainForm>(); 
            return service.Build();
        }
    }
}