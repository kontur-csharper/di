using System;
using System.Drawing;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class CloudLayouterFactory : ICloudLayouterFactory
    {
        private readonly ILayouterAlgorithmSettingsHolder layouterSettings;
        private readonly IImageSizeSettingsHolder sizeSettings;

        public CloudLayouterFactory(ILayouterAlgorithmSettingsHolder layouterSettings, 
            IImageSizeSettingsHolder sizeSettings)
        {
            this.layouterSettings = layouterSettings;
            this.sizeSettings = sizeSettings;
        }
        public ICloudLayouter CreateCloudLayouter()
        {
            switch (layouterSettings.LayouterAlgorithm)
            {
                case CloudLayouterAlgorithm.CircularCloudLayouter:
                    return new CircularCloudLayouter(new Point(sizeSettings.Width / 2,
                        sizeSettings.Height / 2));
            }

            throw new NotImplementedException("No found this algorithm");
        }
    }
}