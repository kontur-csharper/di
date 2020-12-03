using System;
using System.IO;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.WordsParser
{
    public class FileReader : IWordReader
    {
        private static readonly Regex ExtensionRegex = new Regex(@".*?(?<extension>\.[^.]*?)$");
        private readonly ReadWordMethod readWordMethod;
        private readonly StreamReader sr;

        private delegate string ReadWordMethod();

        public FileReader(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File {filePath} not found.");

            sr = new StreamReader(filePath);
            var extension = GetFileExtension(filePath);

            readWordMethod = extension switch
            {
                ".txt" => ReadWordFromTxt,
                _ => throw new ArgumentException($"Can't read {extension} file")
            };
        }

        public string ReadWord() => readWordMethod();

        private string ReadWordFromTxt()
        {
            while (sr.Peek() >= 0)
            {
                var word = sr.ReadLine()?.Trim();
                if (word is null)
                    return null;
                if (word.Length != 0)
                    return word;
            }

            return null;
        }

        private static string GetFileExtension(string filePath) =>
            ExtensionRegex.Match(filePath).Groups["extension"].Value;
    }
}