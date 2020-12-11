using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.TextFileParser;

namespace TagCloud.Visualizer.Console
{
    public static class TextReader
    {
        private static readonly string InputPath = Path.Combine(Directory.GetCurrentDirectory(),
            "..",
            "..",
            "..",
            "..",
            $"{nameof(TagCloud)}.{nameof(Visualizer)}",
            "source");

        private static readonly HashSet<string> WordsToExclude = new HashSet<string>
        {
            "и",
            "а",
            "в",
            "о",
            "от",
            "да",
            "нет",
            "то",
            "с",
            "по",
            "к",
            "около",
            "но",
            "через",
            "что",
            "где",
            "когда",
            "откуда",
            "куда",
            "ну",
            "до",
            "эти",
            "со",
            "же",
            "при",
            "их",
            "он",
            "не",
            "ни",
            "ей",
            "ему",
            "есть",
            "мог",
            "могла",
            "была",
            "вы",
            "за",
            "я",
            "был",
            "быть",
            "есть",
            "у",
            "ты",
            "бы",
            "это",
            "так",
            "из",
            "на",
            "мы",
            "тут",
            "во",
            "ней",
            "нему",
            "сам",
            "него",
            "неё",
            "опять",
            "тем",
            "или",
            "для",
            "мой",
            "эту",
            "вам",
            "про",
            "без",
            "им",
            "ей",
            "неё",
            "кто",
            "над",
            "уж",
            "эта",
            "тот",
            "этот",
            "нею",
            "вот",
            "его",
            "ту",
            "ж",
            "же",
            "т",
            "хоть",
            "таки"
        };

        public static IEnumerable<string> GetWords(InputOptions inputOptions, string sourceFolderPath,
            ITextFileParser fileParser, IWordsHandler wordsHandler)
        {
            return fileParser.TryGetWords(inputOptions.FileName, sourceFolderPath, out var words)
                ? wordsHandler.ProcessWords(words)
                    .Where(word => !WordsToExclude.Contains(word))
                : null;
        }

        public static IEnumerable<string> GetWords(InputOptions inputOption, ITextFileParser fileParser,
            IWordsHandler wordsHandler)
        {
            return GetWords(inputOption,
                InputPath,
                fileParser,
                wordsHandler);
        }
    }
}