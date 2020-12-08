using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autofac;
using TagsCloud.GUI;
using TagsCloud.Infrastructure;

namespace TagsCloud
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var defaultImageSettings = new ImageSettings(1280, 720);
            var defaultWordsToIgnore = new HashSet<string>
                {"а", "и", "да", "но", "как", "то", "либо", "ибо", "ну", "у"};
            var container = DependenciesContainerProvider.GetContainer(defaultImageSettings, defaultWordsToIgnore);
            RunApp(container);
        }

        public static void RunApp(IContainer container)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainForm>());
        }
    }
}
