using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudContainer.Actions;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.Common;
using TagsCloudContainer.TextAnalyzing;
using TagsCloudContainer.UiActions;

namespace TagsCloudContainer
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<PictureBoxImageHolder>().As<IImageHolder, PictureBoxImageHolder>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImageSettings>().InstancePerLifetimeScope();
            builder.RegisterType<FontSettings>().InstancePerLifetimeScope();
            builder.RegisterType<FilesSettings>().InstancePerLifetimeScope();
            builder.RegisterType<SpiralCloudLayouter>().As<ICloudLayouter>();
            builder.RegisterType<TextAnalyzer>();
            builder.RegisterType<TagCreator>();
            builder.RegisterType<AppSettings>();
            builder.RegisterType<Palette>();
            builder.RegisterType<Gradient>();
            builder.RegisterType<RandomColors>();
            builder.RegisterType<ColorSettingsProvider>().InstancePerLifetimeScope();
            builder.RegisterType<ImageSettingsAction>().As<IUiAction>();
            builder.RegisterType<SaveImageAction>().As<IUiAction>();
            builder.RegisterType<FontSettingsAction>().As<IUiAction>();
            builder.RegisterType<GradientSettingsAction>().As<IUiAction>();
            builder.RegisterType<RandomColorsSettingsAction>().As<IUiAction>();
            builder.RegisterType<PaletteSettingsAction>().As<IUiAction>();
            builder.RegisterType<FilesSettingsAction>().As<IUiAction>();
            builder.RegisterType<SpiralTagCloudVisualizationAction>().As<IUiAction>();
            builder.RegisterType<Painter>();
            builder.RegisterType<MainForm>();


            var container = builder.Build();

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Resolve<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}