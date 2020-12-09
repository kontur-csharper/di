using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.Settings
{
    public class LayouterAlgorithmSettings : ILayouterAlgorithmSettingsHolder
    {
        public static readonly LayouterAlgorithmSettings Instance = new LayouterAlgorithmSettings();

        private LayouterAlgorithmSettings()
        {
            LayouterAlgorithm = CloudLayouterAlgorithm.CircularCloudLayouter;
        }

        public CloudLayouterAlgorithm LayouterAlgorithm { get; set; }
    }
}