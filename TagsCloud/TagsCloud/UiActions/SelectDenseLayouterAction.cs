using TagsCloud.Infrastructure;
using TagsCloud.Layouters;

namespace TagsCloud.UiActions
{
    public class SelectDenseLayouterAction : IUiAction
    {
        public string Category => "Алгоритм построения облака";
        public string Name => "Плотное построение";
        public string Description => "Размещает слова плотно, основываясь на границах их размеров";
        private IImageHolder holder;
        private CircularCloudLayouter newLayouter;
        public SelectDenseLayouterAction(IImageHolder holder, CircularCloudLayouter layouter)
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
