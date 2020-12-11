using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud
{
    public static class SizesCreator
    {
        public static SizeWithWord[] CreateSizesArray(IEnumerable<string> words, float fontSize, string fontName)
        {
            var g = Graphics.FromHwnd(IntPtr.Zero);
            var font = new Font(fontName, fontSize);
            var frequencyDict = GetFrequencyDictionary(words);
            var maxSize = frequencyDict.Select(pair => g.MeasureString(pair.Key, font))
                .OrderByDescending(size => size.Width).First();
            var minFrequency = frequencyDict.Min(pair => pair.Value);
            var sizes = new List<SizeWithWord>(frequencyDict.Count);
            foreach (var pair in frequencyDict)
            {
                var weight = (double) pair.Value / minFrequency;
                sizes.Add(new SizeWithWord((maxSize * (float) weight).ToSize(), new Word(pair.Key, weight)));
            }

            return sizes.ToArray();
        }

        private static Dictionary<string, int> GetFrequencyDictionary(IEnumerable<string> words)
        {
            var frequencyDict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequencyDict.ContainsKey(word))
                {
                    frequencyDict[word] += 1;
                }
                else
                {
                    frequencyDict[word] = 1;
                }
            }

            return frequencyDict;
        }
    }
}