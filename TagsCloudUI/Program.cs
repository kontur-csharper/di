using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TagsCloudUI.Extensions;

namespace TagsCloudUI
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(BuildContainer().ConfigureFactories().GetService<Form>());
        }

        private static ServiceProvider BuildContainer()
        {
            return new ServiceCollection()
                .Scan(scan => scan.FromApplicationDependencies().AddClasses().AsSelfWithInterfaces().WithSingletonLifetime())

                .AddSingleton<Form, ConfigWindow>()

                .BuildServiceProvider();
        }
    }
}
