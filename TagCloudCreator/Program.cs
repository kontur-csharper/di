﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TagCloud;
using TagCloud.Enums;
using TagCloud.Interfaces;
using TagCloud.Layouter;
using TagCloud.Visualizer;
using Point = TagCloud.Layouter.Point;
using Size = TagCloud.Layouter.Size;

namespace TagCloudCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new Configuration(
                @"D:\input.txt",
                @"D:\output.png",
                @"D:\stopwords.txt",
                Color.Azure, 
                new System.Drawing.Size(5000, 5000));
            var container = MakeContainer(
                CloudLayouterType.ArithmeticSpiral, 
                ColorScheme.RandomColors, 
                FontScheme.Arial, 
                SizeScheme.Linear,
                configuration);
            var app = container.Resolve<Application>();
        }

        public static IDictionary<Enum, Type> CollectCapabilities()
        {
            return new Dictionary<Enum, Type>
            {
                {CloudLayouterType.ArithmeticSpiral, typeof(CircularCloudLayouter)},
                {ColorScheme.RandomColors, typeof(RandomColorScheme)},
                {FontScheme.Arial, typeof(ArialFontScheme)},
                {SizeScheme.Linear, typeof(LinearSizeScheme)}
            };
        }

        public static IWindsorContainer MakeContainer(
            CloudLayouterType layouterType,
            ColorScheme colorScheme,
            FontScheme fontScheme,
            SizeScheme sizeScheme,
            Configuration configuration)
        {
            var capabilities = CollectCapabilities();
            var container = new WindsorContainer();

            container.Register(
                GetRegistration<IWordFilter, WordFilterByFile>()
                    .WithArgument("path", configuration.StopWordsFile),
                GetRegistration<IFileReader, FileReader>()
                    .WithArgument("path", configuration.InputFile),
                GetRegistration<IVisualizer, VisualizerToFile>()
                    .WithArgument("outputPath", configuration.OutputFile)
                    .WithArgument("backgroundColor", configuration.BackgroundColor)
                    .WithArgument("imageSize", configuration.ImageSize),
                GetRegistration<IStatisticsCollector, StatisticsCollector>(),
                GetRegistration<TagsCloud, TagsCloud>(),
                GetRegistration<Point, Point>(),
                GetRegistration(typeof(ICloudLayouter), capabilities[layouterType]),
                GetRegistration(typeof(IColorScheme), capabilities[colorScheme]),
                GetRegistration(typeof(FontScheme), capabilities[fontScheme]),
                GetRegistration(typeof(ISizeScheme), capabilities[sizeScheme]));

            return container;
        }

        public static ComponentRegistration<object> GetRegistration(Type elementFor, Type by)
        => Component.For(elementFor).ImplementedBy(by);

        public static ComponentRegistration<TFor> GetRegistration<TFor, TBy>()
            where TFor : class
            where TBy : TFor
            => Component.For<TFor>().ImplementedBy<TBy>();
    }
}
