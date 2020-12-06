using System;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.ConvertersAndCheckers
{
    public class DirectoryExistsChecker : IDirectoryChecker
    {
        public string GetExistingDirectory(string path)
        {
            var directoryLength = path.LastIndexOf(Path.DirectorySeparatorChar);
            if (directoryLength == -1 || Directory.Exists(path.Substring(0, directoryLength)))
                return path;
            throw new Exception("Doesn't contain the directory to save file");
        }
    }
}