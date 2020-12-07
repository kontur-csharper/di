using System.Drawing;

namespace TagCloud.Gui.ImageResizing
{
    public class DontModifyImageResizer : IImageResizer
    {
        private readonly IUserNotifier notifier;

        public DontModifyImageResizer(IUserNotifier notifier)
        {
            this.notifier = notifier;
        }

        public Image Resize(Image source, Size newSize)
        {
            if (source.Size != newSize)
                notifier.Notify($"Image size {source.Size} is different with expected {newSize}, saved as it is");

            //клонируем потому что снаружи может быть dispose исходного изображения
            return (Image) source.Clone();
        }
    }
}