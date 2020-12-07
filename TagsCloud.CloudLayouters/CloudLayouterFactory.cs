using TagsCloud.Common;

namespace TagsCloud.CloudLayouters
{
    public class CloudLayouterFactory : ICloudLayouterFactory
    {
        private readonly ISpiralFactory spiralFactory;

        public CloudLayouterFactory(ISpiralFactory spiralFactory)
        {
            this.spiralFactory = spiralFactory;
        }

        public ICircularCloudLayouter CreateCircularLayouter()
        {
            return new CircularCloudLayouter(spiralFactory.Create());
        }
    }
}