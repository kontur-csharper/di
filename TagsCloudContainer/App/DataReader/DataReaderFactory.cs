using System;
using System.IO;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.DataReader
{
    public class DataReaderFactory : IDataReaderFactory
    {
        private readonly InputSettings settings;

        public DataReaderFactory(InputSettings settings)
        {
            this.settings = settings;
        }

        public IDataReader CreateDataReader()
        {
            switch (Path.GetExtension(settings.InputFileName))
            {
                case ".txt":
                    return new TxtFileReader(settings.InputFileName);
                case ".docx":
                    return new WordFileReader(settings.InputFileName);
            }
            throw new NotImplementedException("Unknown input file format");
        }
    }
}