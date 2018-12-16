using System.Drawing;
using TagsCloudContainer.UI;

namespace TagsCloudContainer.Layouting
{
    public class TagsCloudLayouterSettings
    {
        public Point Center { get; }

        public TagsCloudLayouterSettings(IUI ui)
        {
            Center = ui.ApplicationSettings.TagsCloudCenter;
        }
        
        public TagsCloudLayouterSettings(Point center)
        {
            Center = center;
        }
    }
}