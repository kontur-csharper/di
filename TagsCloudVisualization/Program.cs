using System;
using System.Windows.Forms;
using Autofac;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using var container = DependencyInjector.RegisterDependencies();

            Application.Run(container.Resolve<Form>());
        }
    }
}