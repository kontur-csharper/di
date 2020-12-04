using System;
using Autofac;
using TagsCloud.CloudRenderers;
using TagsCloud.ContainerConfigurators;
using TagsCloud.StatisticProviders;
using TagsCloud.WordLayouters;
using TagsCloud.WordReaders;
using IContainer = Autofac.IContainer;

namespace TagsCloud
{
    public static class Program
    {
        public static void Main()
        {
            while (true)
            {
                MakeCloud();
            }
        }

        public static void MakeCloud(IContainer container = null)
        {
            container ??= new ConsoleContainerConfigurator().Configure();
            
            var words = container.Resolve<IWordReader>().ReadWords();
            if (words == null) return;
            
            var statistic = container.Resolve<IStatisticProvider>().GetWordStatistics(words);
            
            var layouter = container.Resolve<IWordLayouter>();
            layouter.AddWords(statistic);
            
            var path = container.Resolve<ICloudRenderer>().RenderCloud();
            Console.WriteLine($"Cloud saved in {path}");
        }
        
        public static void MakeCloud(IContainerConfigurator configurator)
        {
            MakeCloud(configurator.Configure());
        }
    }
}