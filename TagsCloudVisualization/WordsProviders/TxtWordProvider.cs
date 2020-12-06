using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.WordsProviders
{
    public class TxtWordProvider : IWordProvider
    {
        public List<string> GetWords(string filepath)
        {
            var words = new List<string>();

            if (!File.Exists(filepath))
                throw new FileNotFoundException();

            using var sr = new StreamReader(filepath);
            string word;
            while ((word = sr.ReadLine()) != null) words.Add(word);

            return words;
        }
    }
}