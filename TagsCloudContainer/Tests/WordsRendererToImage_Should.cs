using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer
{
    [TestFixture]
    public class WordsRendererToImage_Should
    {
        private TagsCloudContainer container;
        private WordsRendererToImageDebug renderer;

        [Test]
        public void NotChangeRatio_WithDefaultScale()
        {
            var text = GetRandomText(regularWords).WithOneWordPerLine();
            container.AddFromText(text)
                .Render();

            foreach (var info in renderer.OutputInfo)
                GetRatio(GetWordRegularSize(info.Word)).Should()
                    .BeApproximately(GetRatio(info.Word.Rectangle.Size), 0.02f);
        }

        [Test]
        public void AllowCustomScaleForWords()
        {
            renderer.WithScale((info, word) => word.Count * 10);
            var text = GetRandomText(regularWords).WithOneWordPerLine();
            container.AddFromText(text)
                .Render();

            foreach (var info in renderer.OutputInfo)
                GetScale(info).Should().BeApproximately(info.Word.Count * 10, 0.02f);
        }

        [Test]
        public void NotChangeRatio_WithCustomScale()
        {
            var text = GetRandomText(regularWords).WithOneWordPerLine();
            renderer.WithScale((info, word) => 100 + word.Count * 10);
            container.AddFromText(text)
                .Render();

            foreach (var info in renderer.OutputInfo)
                GetRatio(GetWordRegularSize(info.Word)).Should()
                    .BeApproximately(GetRatio(info.Word.Rectangle.Size), 0.02f);
        }

        [Test]
        public void AllowCustomFont()
        {
            var font = new Font("Times New Roman", 32, GraphicsUnit.Pixel);
            renderer.WithFont(font);
            var text = GetRandomText(regularWords).WithOneWordPerLine();
            container.AddFromText(text)
                .Render();

            renderer.OutputInfo.Select(i => i.Font.Name).Should().AllBe(font.Name);
        }

        [Test]
        public void AllowCustomFont_ForWords()
        {
            var font = new Font("Times New Roman", 32, GraphicsUnit.Pixel);
            renderer.WithFont((info, word) => word.Count > 10 ? font : info.Renderer.DefaultFont);
            var text = GetRandomText(regularWords).WithOneWordPerLine();
            container.AddFromText(text)
                .Render();

            renderer.OutputInfo.Where(i => i.Word.Count > 10).Select(i => i.Font.Name)
                .Should().AllBe(font.Name);
            renderer.OutputInfo.Where(i => i.Word.Count <= 10).Select(i => i.Font.Name)
                .Should().AllBe(renderer.DefaultFont.Name);
        }

        [Test]
        public void AllowCustomColor()
        {
            var color = Color.Chartreuse;
            renderer.WithColor(color);
            var text = GetRandomText(regularWords).WithOneWordPerLine();
            container.AddFromText(text)
                .Render();

            renderer.OutputInfo.Select(i => i.Color).Should().AllBeEquivalentTo(color);
        }

        [Test]
        public void AllowCustomColor_ForWords()
        {
            var color = Color.Chartreuse;
            renderer.WithColor((info, word) => word.Count > 10 ? color : info.Renderer.DefaultColor);
            var text = GetRandomText(regularWords).WithOneWordPerLine();
            container.AddFromText(text)
                .Render();

            renderer.OutputInfo.Where(i => i.Word.Count > 10).Select(i => i.Color)
                .Should().AllBeEquivalentTo(color);
            renderer.OutputInfo.Where(i => i.Word.Count <= 10).Select(i => i.Color)
                .Should().AllBeEquivalentTo(renderer.DefaultColor);
        }

        private SizeF GetWordRegularSize(LayoutedWord word)
        {
            return Graphics.FromImage(new Bitmap(1, 1)).MeasureString(word.Word, renderer.DefaultFont);
        }

        private float GetRatio(SizeF size)
        {
            return size.Width / size.Height;
        }

        private float GetScale(WordsRendererToImageDebug.WordRenderingInfo info)
        {
            return info.Rectangle.Height / GetWordRegularSize(info.Word).Height;
        }

        [SetUp]
        public void SetUp()
        {
            renderer = new WordsRendererToImageDebug {AutoSize = false};
            container = new TagsCloudContainer()
                .Rendering(renderer);
        }

        private static TextBuilder GetRandomText(params string[][] words) => new TextBuilder(words);
        private string[] regularWords => TextBuilder.regularWords;
        private string[] wordsToExclude => TextBuilder.wordsToExclude;
    }
}