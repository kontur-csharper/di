using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloud.WordFilters
{
    public class PartsOfSpeechFilter : IWordFilter
    {
        private readonly HashSet<PartsOfSpeech> _boringPartsOfSpeeches;
        private readonly Regex _partOfSpeechRegex = new Regex(@"{(\w+)=(?<partOfSpeech>\w+)", RegexOptions.Compiled);

        public PartsOfSpeechFilter(params PartsOfSpeech[] boringPartsOfSpeeches)
        {
            _boringPartsOfSpeeches = boringPartsOfSpeeches.ToHashSet();
        }

        public IReadOnlyList<string> FilterWords(IEnumerable<string> words)
        {
            var result = new List<string>();
            var myStemProcess = new Process
            {
                StartInfo =
                {
                    FileName = "mystem.exe",
                    Arguments = "-i -n -e cp866",
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                }
            };

            myStemProcess.Start();

            foreach (var word in words)
            {
                myStemProcess.StandardInput.Write($"{word}\n");
                var wordInfo = myStemProcess.StandardOutput.ReadLine();

                if (wordInfo is null)
                    continue;

                var partOfSpeech = _partOfSpeechRegex.Match(wordInfo).Groups["partOfSpeech"].Value;
                if (Enum.TryParse(partOfSpeech, out PartsOfSpeech res) && !_boringPartsOfSpeeches.Contains(res))
                    result.Add(word);
            }

            myStemProcess.StandardInput.Close();
            myStemProcess.WaitForExit();
            myStemProcess.Close();
            myStemProcess.Dispose();

            return result;
        }
    }
}