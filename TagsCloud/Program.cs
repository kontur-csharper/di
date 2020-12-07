using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Autofac;
using TagsCloud.App;
using TagsCloud.Infrastructure;
using TagsCloud.UI;

namespace TagsCloud
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(TagsCloudSettings.DefaultSettings).AsSelf();
            builder.RegisterInstance(new RectanglesLayouter(Point.Empty)).As<IRectanglesLayouter>();
            builder.RegisterType<TagsCloudDrawer>().As<ITagsCloudDrawer>();
            builder.RegisterType<TagsCloudHandler>().AsSelf();
            builder.RegisterType<DocFileReader>().As<IFileReader>();
            builder.RegisterType<TxtFileReader>().As<IFileReader>();
            builder.RegisterInstance(GetExcludedWords()).As<IEnumerable<string>>();
            builder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            builder.RegisterType<BlackListWordsFilter>().As<IWordsFilter>().SingleInstance();
            builder.RegisterType<Mainform>().AsSelf();
            var container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Mainform>());
        }

        private static string[] GetExcludedWords()
        {
            return new[]
            {
                "и",
                "а",
                "в",
                "о",
                "от",
                "да",
                "нет",
                "то",
                "с",
                "по",
                "к",
                "около",
                "но",
                "через",
                "что",
                "где",
                "когда",
                "откуда",
                "куда",
                "ну",
                "до",
                "эти",
                "со",
                "же",
                "при",
                "их",
                "он",
                "не",
                "ни",
                "ей",
                "ему",
                "есть",
                "мог",
                "могла",
                "была",
                "вы",
                "за",
                "я",
                "был",
                "быть",
                "есть",
                "у",
                "ты",
                "бы",
                "это",
                "так",
                "из",
                "на",
                "мы",
                "тут",
                "во",
                "ней",
                "нему",
                "сам",
                "него",
                "неё",
                "опять",
                "тем",
                "или",
                "для",
                "мой",
                "эту",
                "вам",
                "про",
                "без",
                "им",
                "ей",
                "неё",
                "кто",
                "над",
                "уж",
                "эта",
                "тот",
                "этот",
                "нею",
                "вот",
                "его",
                "ту",
                "ж",
                "же",
                "т",
                "хоть",
                "таки"
            };
        }
    }
}