using TagsCloudContainer.UiActions;

namespace TagsCloudContainer.Actions
{
    public class SpiralTagCloudVisualizationAction : IUiAction
    {
        private readonly Painter painter;

        public SpiralTagCloudVisualizationAction(Painter painter)
        {
            this.painter = painter;
        }

        public string Category => "Облако тегов";
        public string Name => "Расположение тегов по спирали";
        public string Description => "Расположение тегов по спирали";

        public void Perform()
        {
            painter.PaintTags();
        }
    }
}