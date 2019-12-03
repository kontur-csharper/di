﻿using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudLayout.CloudLayouters;
using Autofac;
using TextConfiguration;
using TextConfiguration.TextReaders;
using TagsCloudLayout.PointLayouters;
using TextConfiguration.WordFilters;
using TextConfiguration.WordProcessors;

namespace TagsCloudVisualization
{
    public static class Program
    {
        public static void Main()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Register(c => 
                new VisualizatorProperties(new Size(800, 600), FontFamily.GenericMonospace))
                .As<VisualizatorProperties>();
            containerBuilder.RegisterType<RawTextReader>()
                .As<ITextReader>();
            containerBuilder.RegisterType<TextPreprocessor>()
                .As<TextPreprocessor>();
            containerBuilder.RegisterType<BoringWordsFilter>()
                .As<IWordFilter>();
            containerBuilder.RegisterType<EmptyWordFilter>()
                .As<IWordFilter>();
            containerBuilder.RegisterType<ToLowerCaseProcessor>()
                .As<IWordProcessor>();
            containerBuilder.RegisterType<ConstantTextColorProvider>()
                .As<ITextColorProvider>();
            containerBuilder.Register(c => Color.FromArgb(127, 0, 0))
                .As<Color>();
            containerBuilder.RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();
            containerBuilder.RegisterType<ArchimedeanSpiral>()
                .As<ICircularPointLayouter>();
            containerBuilder.Register(c => new Point(400, 300))
                .As<Point>();
            containerBuilder.RegisterType<TagCloudVisualizator>()
                .As<TagCloudVisualizator>();

            var builder = containerBuilder.Build();

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Words.txt");
            var image = builder.Resolve<TagCloudVisualizator>().VisualizeText(filePath);
            image.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "Cloud.png"), ImageFormat.Png);
        }
    }
}
