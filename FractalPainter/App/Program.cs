using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Extensions.Conventions;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();

                // start here
                // container.Bind<TService>().To<TImplementation>();

                container.Bind<PictureBoxImageHolder, IImageHolder, PictureBox>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();
                container.Bind<SettingsManager>().ToSelf().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<KochPainter>().ToSelf();

                container.Bind<IDragonPainterFactory>().ToFactory();

                container.Bind<ImageSettings>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load().ImageSettings);

                container.Bind<IImageDirectoryProvider>().ToMethod(context => context.Kernel.Get<SettingsManager>().Load());

                container.Bind<Form>().To<MainForm>();

                container.Bind(x => x.
                    FromThisAssembly()
                    .SelectAllClasses().InNamespaceOf<ImageSettingsAction>()
                    .BindAllInterfaces());

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(container.Get<Form>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}