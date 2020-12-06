﻿using System;
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
            builder.RegisterInstance(new[]
            {
                "и",
                "a",
                "в"
            }).As<IEnumerable<string>>();
            builder.RegisterType<WordNormalizer>().As<IWordNormalizer>();
            builder.RegisterType<BlackListWordsFilter>().As<IWordsFilter>().SingleInstance();
            builder.RegisterType<Mainform>().AsSelf();
            var container = builder.Build();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<Mainform>());
        }
    }
}