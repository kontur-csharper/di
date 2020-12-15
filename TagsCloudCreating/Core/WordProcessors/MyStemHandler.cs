using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TagsCloudCreating.Core.WordProcessors
{
    public static class MyStemHandler
    {
        public static readonly Dictionary<string, string> BoringWords = new Dictionary<string, string>
        {
            ["Прилагательное"] = "A",
            ["Наречие"] = "ADV",
            ["Местоимение-наречие"] = "ADVPRO",
            ["Числительное-прилагательное"] = "ANUM",
            ["Местоимение-прилагательное"] = "APRO",
            ["Часть сложного слова"] = "COM",
            ["Союз"] = "CONJ",
            ["Междометие"] = "INTJ",
            ["Числительное"] = "NUM",
            ["Частица"] = "PART",
            ["Предлог"] = "PR",
            ["Существительное"] = "S",
            ["Местоимение-существительное"] = "SPRO",
            ["Глагол"] = "V"
        };

        public static IEnumerable<(string word, string speechPart)> GetWordsWithPartsOfSpeech(IEnumerable<string> words)
        {
            const string tempInputFile = "input.txt";
            const string tempOutputFile = "output.json";
            var process = new Process
            {
                StartInfo =
                {
                    FileName = "mystem.exe",
                    UseShellExecute = false,
                    Arguments = $@"-iln --format json {tempInputFile} {tempOutputFile}"
                }
            };

            File.WriteAllLines(tempInputFile, words);

            process.Start();
            process.WaitForExit();
            foreach (var rawJson in File.ReadAllLines(tempOutputFile))
                yield return ParseToWordTypePair(rawJson);

            File.Delete(tempInputFile);
            File.Delete(tempOutputFile);
        }

        private static (string word, string speechPart) ParseToWordTypePair(string rawJson)
        {
            var jsonDoc = JsonDocument.Parse(rawJson);
            var (word, type) = jsonDoc.RootElement
                .GetProperty("analysis")
                .EnumerateArray()
                .Select(GetWordAndTypeTuple)
                .FirstOrDefault();
            return (word, type);

            static (string, string) GetWordAndTypeTuple(JsonElement jsonElement)
            {
                var normalizedWord = jsonElement.GetProperty("lex").GetString();
                var type = jsonElement.GetProperty("gr").GetString().Split(',', '=').First();
                return (normalizedWord, type);
            }
        }
    }
}