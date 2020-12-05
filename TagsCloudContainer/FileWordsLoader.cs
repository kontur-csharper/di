using System;
using System.IO;
using System.Linq;

namespace TagsCloudContainer
{
    public abstract class FileWordsLoader : IWordsLoader
    {
        protected readonly string pathToFile;
        protected abstract string[] SupportedFormats { get; }

        protected FileWordsLoader(string pathToFile)
        {
            if (!File.Exists(pathToFile))
                throw new ArgumentException($"The specified file does not exist: {pathToFile}");
            
            if (!SupportedFormats.Contains(Path.GetExtension(pathToFile)))
                throw new ArgumentException(
                    $"Format of this file isn't supported: {pathToFile}" + 
                    $"Supported formats: {string.Join(", ", SupportedFormats)}");

            this.pathToFile = pathToFile;
        }

        public abstract string[] GetWords();
    }
}