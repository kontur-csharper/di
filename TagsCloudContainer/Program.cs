using System;
using System.Drawing;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using RectanglesCloudLayouter.Core;
using RectanglesCloudLayouter.Interfaces;
using TagsCloudContainer.ConvertersAndCheckers;
using TagsCloudContainer.Interfaces;
using TagsCloudContainer.Reader;
using TagsCloudContainer.Saver;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TagsCloudVisualization;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ConsoleClient();
            if (!client.TryGetUserCommands(args, out var commands))
                return;
            var serviceCollection = new ServiceCollection();
            TemporarySettingsStorage settings;
            try
            {
                settings = TemporarySettingsStorage.From(commands);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            InjectDependencies(serviceCollection, settings);
            serviceCollection.BuildServiceProvider().GetRequiredService<IAppProcessor>().Run();
        }

        private static void InjectDependencies(IServiceCollection serviceCollection,
            TemporarySettingsStorage settingsStorage)
        {
            serviceCollection.AddSingleton<IAppProcessor, AppProcessor>()
                .AddSingleton<IFileReader, FileReader>()
                .AddSingleton<IImageSaver, ImageSaver>()
                .AddSingleton<IStorageSettings>(new StorageSettings(settingsStorage.PathToCustomText,
                    settingsStorage.PathToSave,
                    settingsStorage.ImageFormat))
                .AddSingleton<IVisualizationSettings>(new VisualizationSettings(settingsStorage.ImageSize,
                    settingsStorage.BackgroundColor,
                    settingsStorage.TextColor, settingsStorage.Font))
                .AddSingleton<ITextProcessingSettings>(new TextProcessingSettings(settingsStorage.BoringWords))
                .AddSingleton<IVisualization, Visualization>()
                .AddTransient<ICloudLayouter, CloudLayouter>()
                .AddSingleton<ISpiral>(new ArchimedeanSpiral(new Point(0, 0),
                    new SpiralSettings(settingsStorage.AdditionSpiralAngleFromDegrees, settingsStorage.SpiralStep)))
                .AddTransient<ICloudRadiusCalculator, CloudRadiusCalculator>()
                .AddTransient<IWordTagsLayouter, WordTagsLayouter>()
                .AddSingleton(settingsStorage.Font)
                .AddSingleton<IWordsFrequency, WordsFrequency>()
                .AddSingleton<IWordMeasurer, WordMeasurer>()
                .AddSingleton<IWordsFilter, WordsFilter>()
                .AddSingleton<IWordsFilter, SpeechPartsFilter>()
                .AddSingleton(new[] {"PR", "PART", "INTJ", "CONJ", "ADVPRO", "APRO", "NUM", "SPRO"})
                .AddSingleton<INormalizedWordAndSpeechPartParser, NormalizedWordAndSpeechPartParser>()
                .AddSingleton<ITextConverter>(new MyStemConverter(Path.GetFullPath("mystem.exe"), "-ni"));
        }
    }
}