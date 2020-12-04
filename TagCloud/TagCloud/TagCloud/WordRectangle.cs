using System.Drawing;

namespace TagCloud
{
    public class WordRectangle
    {
        public WordRectangle(Rectangle rectangle, string word)
        {
            Rectangle = rectangle;
            Word = word;
        }

        public Rectangle Rectangle { get; set; }
        public string Word { get; set; }
    }
}