using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NHunspell;
using TagsCloud.Common;
using TagsCloud.FileReader;

namespace TagsCloud.Core
{
    public static class TagsHelper
    {
        public static List<(string, int)> GetWords(string pathToFile, string pathToBoringWords,
            string pathToDictionary, string pathToAffix, IReaderFactory readerFactory)
        {
            IEnumerable<string> mainText;
            Hunspell hunspell;
            HashSet<string> boringWords;

            try
            {
                mainText = GetTextFromFile(pathToFile, readerFactory);
                hunspell = new Hunspell(pathToAffix, pathToDictionary);
                boringWords = TextAnalyzer.GetUniqueWords(GetTextFromFile(pathToBoringWords, readerFactory));
            }
            catch (Exception e)
            {
                throw new ArgumentException("not valid path to file:\n" + e.Message);
            }

            return TextAnalyzer.GetWordByFrequency(
                mainText,
                boringWords,
                hunspell,
                x => x.OrderByDescending(y => y.Value)
                    .ThenByDescending(y => y.Key.Length)
                    .ThenBy(y => y.Key, StringComparer.Ordinal));
        }

        private static IEnumerable<string> GetTextFromFile(string document, IReaderFactory readerFactory)
        {
            var extension = document.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries).Last();
            return readerFactory.GetReader(extension).ReadWords(document);
        }

        public static List<Rectangle> GetRectangles(ICircularCloudLayouter cloud, 
            List<(string, int)> words, Dictionary<int, Font> newFonts, Font font)
        {
            var rectangles = new List<Rectangle>();
            foreach (var word in words)
            {
                var fontSize = word.Item2;
                if (!newFonts.TryGetValue(fontSize, out var newFont))
                {
                    newFont = new Font(font.FontFamily, (int) (font.Size * Math.Log(word.Item2 + 1)), font.Style);
                    newFonts[fontSize] = newFont;
                }

                var rect = cloud.PutNextRectangle(new Size((int)newFont.Size * word.Item1.Length, newFont.Height));
                rectangles.Add(rect);
            }
            return rectangles;
        }
    }
}