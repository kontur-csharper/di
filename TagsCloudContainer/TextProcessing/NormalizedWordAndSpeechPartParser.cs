using System;
using System.Text.RegularExpressions;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.TextProcessing
{
    public class NormalizedWordAndSpeechPartParser : INormalizedWordAndSpeechPartParser
    {
        private static Regex _regex = new Regex(@"\w+{(\w+=\w+).*}");

        public string[] ParseToNormalizedWordAndPartSpeech(string word)
        {
            if (word == null)
                throw new ArgumentException("String must be not null");
            var match = _regex.Match(word);
            return !match.Success ? new string[0] : match.Groups[1].Value.Split('=');
        }
    }
}