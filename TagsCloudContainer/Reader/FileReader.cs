using System;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Reader
{
    public class FileReader : IFileReader
    {
        private readonly IStorageSettings _storageSettings;

        public FileReader(IStorageSettings storageSettings)
        {
            _storageSettings = storageSettings;
        }

        public string GetTextFromFile()
        {
            try
            {
                using var reader = new StreamReader(_storageSettings.PathToCustomText);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Can't read the file", e);
            }
        }
    }
}