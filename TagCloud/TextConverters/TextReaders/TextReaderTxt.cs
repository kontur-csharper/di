using System.IO;

namespace TagCloud.TextConverters.TextReaders
{
    public class TextReaderTxt : ITextReader
    {
       public string Extension { get => ".txt"; }

        public string ReadText(string path)
        {
            if (TryReadFile(path, out var result))
                return result;
            return result;
        }

        private bool TryReadFile(string path, out string result)
        {
            try
            {
                using (var sr = File.OpenText(path))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch
            {
                result = null;
            }
            return result != null;
        }
    }
}
