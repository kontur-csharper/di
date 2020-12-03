using System.Collections.Generic;

namespace TagsCloudContainer.Drawer
{
    public interface IRectangleLayout
    {
        public void DrawLayout();

        public void SaveLayout();

        public void PlaceWords(Dictionary<string, int> words);
    }
}