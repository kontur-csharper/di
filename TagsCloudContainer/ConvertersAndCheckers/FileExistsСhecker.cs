using System;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.ConvertersAndCheckers
{
    public class FileExistsСhecker : IFileExistsСhecker
    {
        public string GetProvenPath(string pathToFile)
        {
            if (File.Exists(pathToFile))
                return pathToFile;
            throw new Exception("Doesn't contain the text file");
        }
    }
}