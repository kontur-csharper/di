using System;
using System.Windows.Forms;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();
                container.Bind<IImageHolder, PictureBoxImageHolder>().To<PictureBoxImageHolder>().InSingletonScope();
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<KochPainter>().ToSelf();
                container.Bind<IDragonPainterFactory>().ToFactory();
                container.Bind<Random>().ToSelf().InTransientScope();
                container.Bind<IObjectSerializer>().To<XmlObjectSerializer>();
                container.Bind<IBlobStorage>().To<FileBlobStorage>();
                container.Bind<AppSettings>()
                    .ToMethod(c => c.Kernel.Get<SettingsManager>().Load())
                    .InSingletonScope();
                container.Bind<ImageSettings>()
                    .ToMethod(c => c.Kernel.Get<AppSettings>().ImageSettings)
                    .InSingletonScope();
                container.Bind<IImageDirectoryProvider>()
                    .ToMethod(c => c.Kernel.Get<AppSettings>())
                    .InSingletonScope();
                container.Bind(x => x.FromThisAssembly()
                    .SelectAllClasses().InheritedFrom<IUiAction>()
                    .BindAllInterfaces());

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                var message = e.ToString();
                MessageBox.Show(message);
            }

            // Я бы сейчас пивка бахнул
            // Я бы тоже въебал так то
        }
    }
}