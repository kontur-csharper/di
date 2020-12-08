using TagsCloud.Infrastructure;
using TagsCloud.Layouters;

namespace TagsCloud.UiActions
{
    public class SelectSpiralLayouterAction : IUiAction
    {
        public string Category => "Алгоритм построения облака";
        public string Name => "Спиральное построение";
        public string Description => "Размещает слова по спирали";
        private IImageHolder holder;
        private SpiralCloudLayouter newLayouter;
        public SelectSpiralLayouterAction(IImageHolder holder, SpiralCloudLayouter layouter)
        {
            this.holder = holder;
            newLayouter = layouter;
        }

        public void Perform()
        {
            holder.ChangeLayouter(newLayouter);
        }
    }
}
