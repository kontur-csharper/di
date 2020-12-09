using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.UiActions;

namespace TagsCloudContainer.App.Actions
{
    internal class CircularCloudAction : IUiAction
    {
        private readonly IImageHolder imageHolder;
        private readonly LayouterAlgorithmSettings settings;

        public CircularCloudAction(IImageHolder imageHolder, LayouterAlgorithmSettings settings)
        {
            this.imageHolder = imageHolder;
            this.settings = settings;
        }

        public MenuCategory Category => MenuCategory.Algorithms;
        public string Name => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();
        public string Description => CloudLayouterAlgorithm.CircularCloudLayouter.GetDescription();

        public void Perform()
        {
            settings.LayouterAlgorithm = CloudLayouterAlgorithm.CircularCloudLayouter;
            imageHolder.GenerateImage();
        }
    }
}