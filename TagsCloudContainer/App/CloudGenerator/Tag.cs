using System.Drawing;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class Tag
    {
        public readonly double FontSize;
        public readonly Point Location;
        public readonly string Word;

        public Tag(string word, double fontSize, Point location)
        {
            Word = word;
            FontSize = fontSize;
            Location = location;
        }
    }
}