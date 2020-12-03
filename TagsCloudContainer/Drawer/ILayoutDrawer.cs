using System.Drawing;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Drawer
{
    public interface ILayoutDrawer
    {
        public void AddRectangle(WordRectangle rectangles);

        public void Draw(Graphics graphics);
    }
}