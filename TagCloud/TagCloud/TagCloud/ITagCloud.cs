using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ITagCloud
    {
        List<WordRectangle> WordRectangles { get; }
        void GenerateTagCloud();
        WordRectangle PutNextWord(string word, Size rectangleSize);
    }
}