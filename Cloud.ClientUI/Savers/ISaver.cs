using System.Drawing;

namespace Cloud.ClientUI
{
    public interface ISaver
    {
        public void SaveImage(Bitmap bitmap, string fileName);
    }
}