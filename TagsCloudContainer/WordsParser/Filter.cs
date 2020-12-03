using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudContainer.ProgramOptions;

namespace TagsCloudContainer.WordsParser
{
    public class Filter : IFilter
    {
        private static readonly List<string> BoringPartsOfSpeech =
            new List<string> {"=CONJ", "=INTJ", "=PART", "=PR"};

        private static readonly Regex MatchWordsInfo = new Regex(@"(?<wordInfo>\w+{.*?})");
        private readonly HashSet<string> boringWords;
        private readonly string mystemLocation;

        public Filter(IFilterOptions options)
        {
            boringWords = options.BoringWords.Select(word => word.Normalize()).ToHashSet();
            mystemLocation = options.MystemLocation;
        }

        public HashSet<string> RemoveBoringWords(HashSet<string> words)
        {
            var wordsInfo = GetWordsInfo(words.ToHashSet(), mystemLocation);
            var filteredWords = words
                .Where(word => !boringWords.Contains(word) && !IsWordBoringPartOfSpeech(word, wordsInfo))
                .ToHashSet();
            return filteredWords;
        }

        private static IEnumerable<string> GetWordsInfo(IEnumerable<string> words, string mystemLocation)
        {
            if (mystemLocation is null)
                return words;

            var mystem = new Process
            {
                StartInfo =
                {
                    FileName = Path.Combine(mystemLocation, "mystem.exe"),
                    Arguments = "-i -n -e cp866",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };
            mystem.Start();

            foreach (var word in words)
                mystem.StandardInput.Write($"{word}\n");
            mystem.StandardInput.Close();

            var wordsInfo = MatchWordsInfo
                .Matches(mystem.StandardOutput.ReadToEnd())
                .Select(m => m.Groups["wordInfo"].Value)
                .ToList();
            mystem.WaitForExit();
            mystem.Close();
            mystem.Dispose();
            return wordsInfo;
        }

        private static bool IsWordBoringPartOfSpeech(string word, IEnumerable<string> wordsInfo)
        {
            var boringWordInfos = wordsInfo
                .Where(wordInfo => wordInfo.Contains(word))
                .Where(wordInfo => BoringPartsOfSpeech.Any(wordInfo.Contains))
                .ToList();
            return boringWordInfos.Count != 0;
        }
    }
}