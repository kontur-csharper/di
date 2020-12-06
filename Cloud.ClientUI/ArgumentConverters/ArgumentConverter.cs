using System;
using System.Drawing;
using System.Linq;
using CloudContainer;

namespace Cloud.ClientUI.ArgumentConverters
{
    public class ArgumentConverter : IArgumentConverter
    {
        public TagCloudArguments ConvertArguments(Arguments arguments)
        {
            var result = new TagCloudArguments();
            SetCenter(arguments, result);
            SetFont(arguments, result);
            SetImageSize(arguments, result);
            SetColor(arguments, result);
            SetWords(arguments, result);
            SetInputFileName(arguments, result);
            SetOutputFileName(arguments, result);
            PrintArguments(result);
            return result;
        }

        private void SetInputFileName(Arguments arguments, TagCloudArguments Convertedarguments)
        {
            if (!string.IsNullOrEmpty(arguments.InputFileName))
            {
                Convertedarguments.InputFileName = arguments.InputFileName;
                return;
            }

            Console.WriteLine("Неверно задано значение имени файла со словами, установлено дефолтное значение");
        }

        private void SetOutputFileName(Arguments arguments, TagCloudArguments Convertedarguments)
        {
            if (!string.IsNullOrEmpty(arguments.OutputFileName))
                Convertedarguments.OutputFileName = arguments.OutputFileName;

            Console.WriteLine("Неверно задано значение имени итогового файла, установлено дефолтное значение");
        }

        private void SetWords(Arguments arguments, TagCloudArguments Convertedarguments)
        {
            if (arguments.BoringWords == null)
            {
                Console.WriteLine("Неверно заданы скучные слова, устанолвено дефолтное значение");
                return;
            }

            var words = arguments.BoringWords.Split(',');
            Convertedarguments.BoringWords = words.ToHashSet();
        }

        private void PrintArguments(TagCloudArguments arguments)
        {
            Console.WriteLine("Итоговый конфиг:");
            var font = $"\tНазвание шрифта: {arguments.Font.Name}, размер шрифта: {arguments.Font.Size}";
            var size = $"\tРазмер изображения: {arguments.ImageSize.ToString()}";
            var color = $"\tЦвет слов: {arguments.TextColor.ToString()}";
            var center = $"\tЦентр изображения: {arguments.Center.ToString()}";
            var boringWords = $"\tСкучные слова: {string.Join(", ", arguments.BoringWords)}";
            var inputFileName = $"\tИмя файла со словами: {arguments.InputFileName}";
            var outputFileName = $"\tИмя итогового файла: {arguments.OutputFileName}";
            Console.WriteLine(font);
            Console.WriteLine(size);
            Console.WriteLine(color);
            Console.WriteLine(center);
            Console.WriteLine(boringWords);
            Console.WriteLine(inputFileName);
            Console.WriteLine(outputFileName);
        }

        private void SetCenter(Arguments arguments, TagCloudArguments Convertedarguments)
        {
            if (arguments.XValue == null || arguments.YValue == null)
            {
                Console.WriteLine("Один из параметров центра изображения был неверным, установлено дефолтное значение");
                return;
            }

            var isCorrectX = int.TryParse(arguments.XValue, out var x);
            var isCorrectY = int.TryParse(arguments.YValue, out var y);
            if (isCorrectX && isCorrectY)
            {
                Convertedarguments.Center = new Point(x, y);
                return;
            }

            Console.WriteLine("Один из параметров центра изображения был неверным, установлено дефолтное значение");
        }

        private void SetFont(Arguments arguments, TagCloudArguments Convertedarguments)
        {
            if (arguments.FontName == null || arguments.FontSize == null)
            {
                Console.WriteLine("Один из параметров шрифта был неверным, установлено дефолтное значение");
                return;
            }

            var isCorrectSize = int.TryParse(arguments.FontSize, out var size);
            if (isCorrectSize)
            {
                Convertedarguments.Font = new Font(arguments.FontName, size);
                return;
            }

            Console.WriteLine("Один из параметров шрифта был неверным, установлено дефолтное значение");
        }

        private void SetImageSize(Arguments arguments, TagCloudArguments Convertedarguments)
        {
            if (arguments.ImageHeight == null || arguments.ImageWidth == null)
            {
                Console.WriteLine(
                    "Один из параметров размера изображения был неверным, установлено дефолтное значение");
                return;
            }

            var isCorrectWidth = int.TryParse(arguments.ImageWidth, out var width);
            var isCorrectHeight = int.TryParse(arguments.ImageHeight, out var height);
            if (isCorrectHeight && isCorrectWidth)
            {
                Convertedarguments.ImageSize = new Size(width, height);
                return;
            }

            Console.WriteLine("Один из параметров размера изображения был неверным, установлено дефолтное значение");
        }

        private void SetColor(Arguments arguments, TagCloudArguments Convertedarguments)
        {
            if (arguments.Color == null)
                return;
            try
            {
                Convertedarguments.TextColor = Color.FromName(arguments.Color);
            }
            catch
            {
                Console.WriteLine("Неправильное название цвета, установлено дефолтное значение");
            }
        }
    }
}