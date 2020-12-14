using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud.Factory
{
    public class CloudDrawerFactory : ICloudDrawerFactory
    {
        public ICloudDrawer Get(Size pictureSize)
        {
            return new CloudDrawer(pictureSize);
        }
    }
}