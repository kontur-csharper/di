using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WordCloudGenerator
{
    public class ConsoleUI : IUserInterface
    {
        private readonly IPreparer.Factory preparerFactory;
        private readonly IPalette.Factory paletteFactory;
        private readonly IPainter.Factory painterFactory;

        public ConsoleUI(IPreparer.Factory preparerFactory, IPalette.Factory paletteFactory, IPainter.Factory painterFactory)
        {
            this.preparerFactory = preparerFactory;
            this.paletteFactory = paletteFactory;
            this.painterFactory = painterFactory;
        }

        public void Run()
        {
            var prepared = PrepareWords();

            var graphicStrings = GenerateCloud(prepared);

            var palette = EnterPalette();
            var painter = painterFactory(palette);
            var img = PerformAndReport("Рисование облака", () => painter.Paint(graphicStrings));

            var pathToSave = AskUntilCorrect("путь по которому сохранить изображение", Saver.IsPathCorrect,
                "путь неверный");
            Saver.SaveImage(img, pathToSave);

            Console.WriteLine($"Изображение сохранено в {pathToSave}");
        }

        private IPalette EnterPalette()
        {
            var colorConverter = new ColorConverter();
            
            // ReSharper disable once PossibleNullReferenceException
            // cant be null because of CanConvertFrom
            var bgColor = (Color) colorConverter.ConvertFromString(AskUntilCorrect("цвет фона",
                colorConverter.CanConvertFrom, "такого цвета не существует").Trim());

            var colorsStr =
                RepeatUntil(() => AskUntilCorrect("цвета палитры (пустая строка чтобы прекратить)",
                    colorConverter.CanConvertFrom,
                    "такого цвета не существует"), string.IsNullOrEmpty);
            var mainColors = colorsStr.Select(colorConverter.ConvertFromString).Cast<Color>();

            var palette = paletteFactory(mainColors, bgColor);
            return palette;
        }

        private IEnumerable<GraphicString> GenerateCloud(IEnumerable<WordFrequency> prepared)
        {
            var algoChoice = AskUntilCorrect(
                "какой алгоритм использовать - экспоненциальный или пропорциональный? (1/2)",
                str => str == "1" || str == "2", "нужно выбрать 1 или 2");
            var algorithm = AlgorithmFabric.Create((AlgorithmType) int.Parse(algoChoice));
            return algorithm(prepared);
        }

        private IEnumerable<WordFrequency> PrepareWords()
        {
            var pathToText = Ask("полный путь к файлу с текстом");
            var text = Reader.ReadFile(pathToText);
            var wordsToSkip = RepeatUntil(
                () => Ask("Введите слово которое нужно пропускать (пустая строка, чтобы закончить)"),
                string.IsNullOrEmpty);

            var preparer = preparerFactory(wordsToSkip);
            var wordCount = int.Parse(AskUntilCorrect("сколько слов должно быть в облаке",
                str => int.TryParse(str, out _), "это не является числом"));
            
            return PerformAndReport("Обработка текста", () => preparer.CreateWordFreqList(text, wordCount));
        }

        private string Ask(string what)
        {
            Console.WriteLine($"Введите {what}");

            return Console.ReadLine();
        }

        private string AskUntilCorrect(string what, Func<string, bool> isCorrect, string ifErr)
        {
            var read = Ask(what);
            while (!isCorrect(read))
            {
                Console.WriteLine($"{ifErr}, введите снова");
                read = Console.ReadLine();
            }

            return read;
        }

        private IEnumerable<T> RepeatUntil<T>(Func<T> get, Func<T, bool> shouldStop)
        {
            while (true)
            {
                var getted = get();
                if (shouldStop(getted))
                    yield break;

                yield return getted;
            }
        }

        private T PerformAndReport<T>(string actionName, Func<T> func)
        {
            Console.Write($"{actionName} выполняется");
            var result = func();
            ClearLine();
            Console.WriteLine($"{actionName} закончена");
            return result;
        }

        private void ClearLine()
        {
            Console.Write('\r' + new string(' ', Console.BufferWidth));
        }
    }
}