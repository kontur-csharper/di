using System.Windows.Forms;
using Autofac;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Contracts;
using TagsCloudCreating.Core;
using TagsCloudCreating.Core.CircularCloudLayouter;
using TagsCloudCreating.Core.ColorizeAlgorithms;
using TagsCloudCreating.Core.WordProcessors;
using TagsCloudVisualization.App;
using TagsCloudVisualization.Contracts;
using TagsCloudVisualization.Infrastructure.Common;
using TagsCloudVisualization.MenuItems;
using TagsCloudVisualization.MenuItems.Settings;

namespace TagsCloudVisualization.Infrastructure
{
    public static class DependencyInjector 
    {
        public static IContainer RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            
            RegisterAppSettings(builder);
            RegisterTagCloudInfrastructure(builder);
            RegisterMenuItems(builder);
            
            builder.RegisterType<TagsCloudPictureHolder>().AsSelf().SingleInstance();
            builder.RegisterType<MainForm>().As<Form>();

            return builder.Build();
        }

        private static void RegisterMenuItems(ContainerBuilder builder)
        {
            builder.RegisterType<TagsCloudSaver>().As<IMenuItem>();
            builder.RegisterType<TagsCloudVisualizer>().As<IMenuItem>();
            builder.RegisterType<BoringWordsSelector>().As<IMenuItem>();
            builder.RegisterType<CloudLayouterSettingsMenuItem>().As<IMenuItem>();
            builder.RegisterType<ImageSettingsMenuItem>().As<IMenuItem>();
            builder.RegisterType<TagsSettingsMenuItem>().As<IMenuItem>();
        }

        private static void RegisterTagCloudInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterType<RandomColorizer>().AsSelf().As<IColorizer>().SingleInstance();
            builder.RegisterType<SteelForHumans>().AsSelf().As<IColorizer>().SingleInstance();
            builder.RegisterType<FileWordsReader>().As<IWordsReader>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().As<ITagsCloudLayouter>().SingleInstance();
            builder.RegisterType<TagsCloudCreator>().As<ITagsCloudCreator>().SingleInstance();
            builder.RegisterType<WordHandler>().As<IWordHandler>().SingleInstance();
            builder.RegisterType<WordConverter>().AsSelf().SingleInstance();
        }

        private static void RegisterAppSettings(ContainerBuilder builder)
        {
            builder.RegisterInstance(SettingsSerializer.Deserialize()).As<SettingsManager>().SingleInstance();
            
            builder
                .Register(context => context.Resolve<SettingsManager>().ImageSettings)
                .As<ImageSettings>()
                .SingleInstance();
            builder
                .Register(context => context.Resolve<SettingsManager>().WordHandlerSettings)
                .As<WordHandlerSettings>()
                .SingleInstance();
            builder
                .Register(context => context.Resolve<SettingsManager>().LayouterSettings)
                .As<CloudLayouterSettings>()
                .SingleInstance();
            builder
                .Register(context => context.Resolve<SettingsManager>().TagsSettings)
                .As<TagsSettings>()
                .SingleInstance();
        }
    }
}