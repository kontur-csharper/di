using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface ICloudDrawerFactory
    {
        ICloudDrawer Get(Size pictureSize);
    }
}