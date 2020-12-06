using System.Drawing;

namespace TagsCloudContainer.Interfaces
{
    public interface IWordMeasurer
    {
        Size GetWordSize(string word, Font font);
    }
}