using System.Collections.Generic;
using Autofac;
using TagsCloud.GUI;
using TagsCloud.Layouters;
using TagsCloud.UiActions;
using TagsCloud.WordsProcessing;

namespace TagsCloud.Infrastructure
{
    public static class DependenciesContainerProvider
    {
        public static IContainer GetContainer(ImageSettings imageSettings, HashSet<string> wordsToExclude)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PictureBoxImageHolder>().AsSelf().As<IImageHolder>().SingleInstance();
            builder.RegisterType<CircularCloudLayouter>().AsSelf().As<ICloudLayouter>().SingleInstance();
            builder.RegisterType<SpiralCloudLayouter>().AsSelf().As<ICloudLayouter>().SingleInstance();
            builder.Register(_ => imageSettings).SingleInstance();
            builder.Register(_ => new WordsFilter(wordsToExclude)).AsSelf().As<IWordsFilter>().SingleInstance();
            builder.RegisterType<WordsFrequencyParser>().As<IWordsFrequencyParser>();

            builder.RegisterType<RenderFileAction>().As<IUiAction>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            builder.Register(_ => new ParserSettingsAction(wordsToExclude)).As<IUiAction>().SingleInstance();
            builder.RegisterType<SelectDenseLayouterAction>().As<IUiAction>();
            builder.RegisterType<SelectSpiralLayouterAction>().As<IUiAction>();
            builder.RegisterType<MainForm>().AsSelf();

            return builder.Build();
        }
    }
}
