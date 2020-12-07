using System.Drawing;
using System.Drawing.Imaging;

namespace TagCloud.Core.Output
{
    public interface IFileResultWriter
    {
        void Save(Image result, ImageFormat format, string path);
    }
}