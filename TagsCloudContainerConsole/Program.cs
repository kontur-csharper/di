using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using CommandLine;
using LightInject;
using TagsCloudVisualization;
using WeCantSpell.Hunspell;

namespace TagsCloudContainer
{
    class Program
    {
        private static AppSettings _appSettings;
        private static readonly string _pathToDict = "Dictionaries/English (American).dic";
        private static readonly string _pathToAffixFile = "Dictionaries/English (American).aff";

        static void Main(string[] args)
        {
            try
            {
                var settings = Parser.Default.ParseArguments<AppSettings>(args);
                if (settings.Tag == ParserResultType.NotParsed)
                    throw new ArgumentException("Incorrect commandline arguments");

                _appSettings = ((Parsed<AppSettings>) settings).Value;
                
                var loader = GetWordsLoader();
                var words = loader.GetWords();
                
                var countedWords = CountWords(words);
                
                var drawer = GetDrawer();
                var image = drawer.Draw(countedWords);
                SaveImage(image);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        private static IWordsLoader GetWordsLoader()
        {
            return Path.GetExtension(_appSettings.PathToFile) switch
            {
                ".docx" => new DocxFileWordsLoader(_appSettings.PathToFile),
                ".txt" => new TxtFileWordsLoader(_appSettings.PathToFile),
                var format => throw new ArgumentException($"Not supported format: {format}")
            };
        }

        private static Dictionary<string, int> CountWords(string[] words)
        {
            var excludedPartsOfSpeech = new List<PartOfSpeech>();
            foreach (var name in _appSettings.ExcludedPartsOfSpeechNames)
            {
                if (Enum.TryParse<PartOfSpeech>(name, true, out var partOfSpeech))
                    excludedPartsOfSpeech.Add(partOfSpeech);
                else
                    throw new ArgumentException($"Incorrect name of part of speech: {name}");
            }

            var appDirectory = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);
            var wordList = WordList.CreateFromFiles( 
                Path.GetFullPath(_pathToDict, appDirectory),
                Path.GetFullPath(_pathToAffixFile, appDirectory));
            
            return new MorphologicalWordsCounter(wordList, excludedPartsOfSpeech).CountWords(words);
        }

        private static ITagsCloudDrawer GetDrawer()
        {
            var container = new ServiceContainer();

            container.Register<IFontColorCreator>(factory =>
                new FontColorCreator(_appSettings.FontColor));
            
            container.Register<IFontCreator>(factory =>
                new FontCreator(_appSettings.FontName));

            var center = new Point(_appSettings.ImageWidth/2, _appSettings.ImageHeight/2);
            var angleInRadians = _appSettings.AngleStepInDegrees * Math.PI / 180;
            container.Register<ICloudLayouter>(factory =>
                new CircularCloudLayouter(center, new Spiral(angleInRadians, _appSettings.ShiftFactor)));
            
            container.Register<ITagsCloudSettings>(factory => _appSettings);
            
            container.Register<ITagsCloudDrawer, TagsCloudDrawer>();

            return container.GetInstance<ITagsCloudDrawer>();
        }

        private static void SaveImage(Bitmap image)
        {
            new ImageSaver(_appSettings.ImagePath).Save(image);
        }
    }
}