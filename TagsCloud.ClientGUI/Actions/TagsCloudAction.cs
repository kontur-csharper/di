using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Common;

namespace TagsCloud.ClientGUI.Actions
{
    public class TagsCloudAction : IUiAction
    {
        private readonly ICloudLayouterFactory cloudFactory;
        private readonly TagsCloudPainter tagsCloudPainter;

        public TagsCloudAction(TagsCloudPainter tagsCloudPainter, ICloudLayouterFactory cloudFactory)
        {
            this.tagsCloudPainter = tagsCloudPainter;
            this.cloudFactory = cloudFactory;
        }

        public string Category => "Облако тегов";
        public string Name => "Нарисовать облако тегов";
        public string Description => "Рисование облака тегов по спирали";

        public void Perform()
        {
            tagsCloudPainter.Paint(cloudFactory.CreateCircularLayouter());
        }
    }
}