using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace TagCloud.Core.Output
{
    public class FileResultWriter : IFileResultWriter
    {
        public void Save(Image result, ImageFormat format, string path)
        {
            using var writer = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            result.Save(writer, format);
        }
    }
}