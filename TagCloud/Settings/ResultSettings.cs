using System.IO;

namespace TagCloud.Settings
{
    public class ResultSettings
    {
        private string outputPath;

        public string OutputPath
        {
            get => outputPath;
            set
            {
                if (!Directory.Exists(value))
                    Directory.CreateDirectory(value);
                outputPath = value;
            }
        }

        public string Name { get; set; }

        public string FileName => Path.Combine(OutputPath, Name);
    }
}
