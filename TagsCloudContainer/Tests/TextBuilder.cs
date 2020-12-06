using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsCloudContainer
{
    public class TextBuilder
    {
        private List<string> words;
        private int wordsCount = 100;
        private Random random = new Random();

        public static string[] regularWords =
        {
            "массив", "набор", "куча", "обычных", "слов", "существительные", "прилагательные", "глаголы"
        };

        public static string[] wordsToExclude =
        {
            "ты", "вы", "он", "она", "они"
        };

        public TextBuilder(params string[][] words)
        {
            this.words = words.SelectMany(w => w).ToList();
        }

        public TextBuilder WithWords(params string[][] words)
        {
            this.words.AddRange(words.SelectMany(w => w));
            return this;
        }

        public TextBuilder WithWordsCount(int count)
        {
            wordsCount = count;
            return this;
        }

        public TextBuilder WithLowerCase()
        {
            words = words.Select(w => w.ToLower()).ToList();
            return this;
        }

        public TextBuilder WithLowerOrTitleCase()
        {
            words = words.Select(WithLowerOrTitleCase).ToList();
            return this;
        }

        private string WithLowerOrTitleCase(string word)
        {
            switch (random.Next(2))
            {
                case 0: return word.ToLower();
                case 1: return ToTitle(word);
                default: return word;
            }
        }

        public string WithOneWordPerLine()
        {
            var builder = new StringBuilder();
            var words = GetRandomWords(wordsCount);
            foreach (var word in words)
            {
                builder.AppendLine(word);
            }

            return builder.ToString();
        }

        public static string ToTitle(string word) => char.ToUpper(word[0]) + word.Substring(1).ToLower();

        private IEnumerable<string> GetRandomWords(int count)
        {
            for (var i = 0; i < count; i++)
                yield return words[random.Next(words.Count)];
        }
    }
}