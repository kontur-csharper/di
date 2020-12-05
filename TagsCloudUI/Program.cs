using System;
using System.Windows.Forms;
using Autofac;

namespace TagsCloudUI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<TagsCloudUiModule>();
            var container = containerBuilder.Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<TagsCloudForm>());
        }
    }
}