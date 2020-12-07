using System.Drawing;
using TagCloud.Core.Utils;

namespace TagCloud.Gui.ImageResizing
{
    public class PlaceAtCenterImageResizer : IImageResizer
    {
        private readonly IUserNotifier notifier;

        public PlaceAtCenterImageResizer(IUserNotifier notifier)
        {
            this.notifier = notifier;
        }

        public Image Resize(Image source, Size newSize)
        {
            if (source.Width > newSize.Width || source.Height > newSize.Height)
            {
                notifier.Notify($"Result image is bigger {source.Size} than specified size {newSize}. " +
                                "Image will be compressed and may contain artefacts");
                return GraphicsUtils.FitToSize(source, newSize);
            }

            return GraphicsUtils.PlaceAtCenter(source, newSize);
        }
    }
}