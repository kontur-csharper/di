using System;
using System.Drawing;
using Autofac;

namespace WordCloudGenerator
{
    public static class Program
    {
        public static void Main()
        {
            var container = Configure();
            container.Resolve<IUserInterface>().Run();
        }

        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Preparer>().As<IPreparer>().WithParameter("filter", new Func<string, bool>(str => str.Length >= 3));
            builder.RegisterType<Painter>().As<IPainter>();
            builder.RegisterInstance(FontFamily.GenericSansSerif).As<FontFamily>();

            builder.RegisterType<OrderedChoicePalette>().As<IPalette>();
            builder.RegisterInstance(new CircularLayouter(new Point(0,0))).As<ILayouter>();
            builder.RegisterType<ConsoleUI>().As<IUserInterface>();


            return builder.Build();
        }
    }
}