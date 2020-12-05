using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class TagsCloudDrawer : ITagsCloudDrawer
    {
        private readonly ITagsCloudSettings settings;
        private readonly ICloudLayouter layouter;
        private readonly IFontCreator fontCreator;
        private readonly IFontColorCreator colorCreator;
        private readonly List<Font> fontsCache = new List<Font>();
        private readonly SolidBrush brush = new SolidBrush(Color.Black);

        public TagsCloudDrawer(ITagsCloudSettings settings, ICloudLayouter layouter,
            IFontCreator fontCreator, IFontColorCreator colorCreator)
        {
            this.settings = settings;
            this.layouter = layouter;
            this.fontCreator = fontCreator;
            this.colorCreator = colorCreator;
        }

        public Bitmap Draw(Dictionary<string, int> countedWords)
        {
            var bitmap = new Bitmap(settings.ImageWidth, settings.ImageHeight);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);
            var maxWordsCount = countedWords.Select(pair => pair.Value).Max();

            foreach (var (word, wordsCount) in countedWords)
            {
                DrawWord(word, wordsCount, maxWordsCount, graphics);
            }
            DisposeFonts();

            return bitmap;
        }

        private void DrawWord(string word, int count, int maxWordsCount, Graphics graphics)
        {
            var font = GetFont(count, maxWordsCount);
            brush.Color = colorCreator.GetFontColor(count, maxWordsCount);
            var rectSize = Size.Ceiling(graphics.MeasureString(word, font));
            var wordRect = layouter.PutNextRectangle(rectSize);

            graphics.DrawString(word, font, brush, wordRect);
        }

        private Font GetFont(int count, int maxWordsCount)
        {
            var fontName = fontCreator.GetFontName(count, maxWordsCount);
            var size = fontCreator.GetFontSize(count, maxWordsCount);
            var font = fontsCache.FirstOrDefault(font =>
                font.Name == fontName && font.Size == size);

            if (font != null)
                return font;

            font = new Font(fontName, size);
            fontsCache.Add(font);
            return font;
        }

        private void DisposeFonts()
        {
            foreach (var font in fontsCache)
            {
                font.Dispose();
            }
        }
    }
}