using System.Drawing;

namespace TagsCloudContainer.Infrastructure.CloudGenerator
{
    internal interface IFontGetter
    {
        public Font GetFont(string word, double frequency);
    }
}