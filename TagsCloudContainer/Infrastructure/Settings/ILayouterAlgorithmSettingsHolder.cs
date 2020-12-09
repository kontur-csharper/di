using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.Infrastructure.Settings
{
    interface ILayouterAlgorithmSettingsHolder
    {
        public CloudLayouterAlgorithm LayouterAlgorithm { get; }
    }
}
