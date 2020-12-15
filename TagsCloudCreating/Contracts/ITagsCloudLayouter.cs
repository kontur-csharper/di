using System.Drawing;

namespace TagsCloudCreating.Contracts
{
    public interface ITagsCloudLayouter
    {
        public Rectangle PutNextRectangle(Size size);
        public void Recreate();
    }
}