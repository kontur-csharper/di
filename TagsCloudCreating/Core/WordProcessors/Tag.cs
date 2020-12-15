using System.Drawing;
using TagsCloudCreating.Configuration;

namespace TagsCloudCreating.Core.WordProcessors
{
    public class Tag
    {
        public string Word { get; }
        public int Frequency { get; }
        public Font Font { get; }
        public Color Color { get; }
        public Rectangle Frame { get; private set; }

        public Tag(string word, int frequency, TagsSettings tagsSettings)
        {
            Word = word;
            Frequency = frequency;
            var fontSize = (frequency + tagsSettings.Font.Size) * 2;
            Font = new Font(tagsSettings.Font.Name, fontSize);
            Color = tagsSettings.Colorizer.Paint();
        }

        public Tag InsertTagInFrame(Rectangle frame)
        {
            Frame = frame;
            return this;
        }

        public Size GetCeilingSize()
        {
            using var fakeGraphics = Graphics.FromImage(new Bitmap(1, 1));
            return Size.Round(fakeGraphics.MeasureString(Word, Font)) + new Size(1, 1);
        }
    }
}