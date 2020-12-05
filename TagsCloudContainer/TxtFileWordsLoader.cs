using System.IO;

namespace TagsCloudContainer
{
    public class TxtFileWordsLoader : FileWordsLoader
    {
        protected override string[] SupportedFormats { get; } = {".txt"};

        public TxtFileWordsLoader(string pathToFile) : base(pathToFile)
        {
        }

        public override string[] GetWords()
        {
            return File.ReadAllLines(pathToFile);
        }
    }
}