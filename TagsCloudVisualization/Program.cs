using System;
using TagsCloudVisualization.Canvases;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using TagsCloudVisualization.AppSettings;
using TagsCloudVisualization.FormAction;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.PointsGenerators;
using TagsCloudVisualization.TagCloudBuilders;
using TagsCloudVisualization.TagCloudLayouter;
using TagsCloudVisualization.TagCloudVisualizers;
using TagsCloudVisualization.TextProcessing.Readers;
using TagsCloudVisualization.TextProcessing.TextHandler;
using TagsCloudVisualization.TextProcessing.TextReader;
using TagsCloudVisualization.WordsProcessing.WordsFilters;
using TagsCloudVisualization.WordsProcessing.WordsWeighers;

namespace TagsCloudVisualization
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICloudLayouter, CircularCloudLayouter>();
            services.AddSingleton<ICloudVisualizer, TagCloudVisualizer>();
            services.AddSingleton<ITagCloudBuilder, TagCloudBuilder>();
            services.AddSingleton<ITextReader, TextReader>();
            services.AddSingleton<ITextHandler, FrequencyTextHandler>();
            services.AddSingleton<IWordsWeigher, FrequencyWordWeigher>();
            services.AddSingleton<IImageSaver, ImageSaver>();
            services.AddSingleton<ICanvas, Canvas>();

            services.AddSingleton<IWordFilter, ForbiddenWordFilter>();
            services.AddSingleton<IWordFilter, LengthFilter>();
            
            services.AddSingleton<IReader, TxtReader>();
            services.AddSingleton<IReader, MSWordReader>();
            
            services.AddSingleton<SpiralParams>();
            services.AddSingleton<FontSettings>();
            services.AddSingleton<ImageSettings>();
            services.AddSingleton<PaintingSettings>();
            services.AddSingleton<WordsSettings>();

            services.AddSingleton<IPointGenerator, ArchimedesSpiral>(x =>
                new ArchimedesSpiral(x.GetRequiredService<SpiralParams>(), x.GetRequiredService<ICanvas>().GetImageCenter()));
            
            services.AddTransient<IFormAction, SaveImageAction>();
            services.AddTransient<IFormAction, CloudLayouterAction>();
            services.AddTransient<IFormAction, FontAction>();
            services.AddTransient<IFormAction, PaletteAction>();
            services.AddTransient<IFormAction, ImageSettingsAction>();
            services.AddTransient<IFormAction, ExampleAction>();
            services.AddTransient<IFormAction, ForbiddenWordsAction>();

            services.AddSingleton<Form, MainForm>();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(services.BuildServiceProvider().GetRequiredService<Form>());
        }
    }
}