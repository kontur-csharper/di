using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public class WordRendererToImage : IParameterizedWordRendererToImage
    {
        public Font DefaultFont = new Font("Arial", 32, GraphicsUnit.Pixel);
        public Color DefaultColor = Color.Red;

        private Image output;
        private Graphics graphics;

        private Func<RenderingInfo, LayoutedWord, Font> fontFunction = DefaultFontFunction;
        private Func<RenderingInfo, LayoutedWord, Color> colorFunction = DefaultColorFunction;
        private Func<SizingInfo, LayoutedWord, float> scaleFunction = (info, word) => word.Count;

        public bool AutoSize { get; set; }

        public Image Output
        {
            get => output;
            set
            {
                output = value;
                graphics = Graphics.FromImage(output);
            }
        }

        public WordRendererToImage()
        {
            Output = new Bitmap(1, 1);
            AutoSize = true;
        }

        public WordRendererToImage(Image output)
        {
            Output = output;
        }

        public WordRendererToImage WithFont(Func<RenderingInfo, LayoutedWord, Font> fontFunc)
        {
            fontFunction = fontFunc;
            return this;
        }

        public WordRendererToImage WithDefaultFont(Font defaultFont)
        {
            DefaultFont = defaultFont;
            fontFunction = (info, word) => info.Renderer.DefaultFont;
            return this;
        }

        public WordRendererToImage WithFont(Font defaultFont)
            => WithDefaultFont(defaultFont).WithFont((info, word) => info.Renderer.DefaultFont);

        public WordRendererToImage WithColor(Func<RenderingInfo, LayoutedWord, Color> colorFunc)
        {
            colorFunction = colorFunc;
            return this;
        }

        public WordRendererToImage WithDefaultColor(Color defaultColor)
        {
            DefaultColor = defaultColor;
            return this;
        }

        public WordRendererToImage WithColor(Color defaultColor)
            => WithDefaultColor(defaultColor).WithColor((info, word) => info.Renderer.DefaultColor);

        public WordRendererToImage WithScale(Func<SizingInfo, LayoutedWord, float> scaleFunc)
        {
            scaleFunction = scaleFunc;
            return this;
        }

        public void SetFontFunction(Func<RenderingInfo, LayoutedWord, Font> fontFunc) => WithFont(fontFunc);
        public void SetScalingFunction(Func<SizingInfo, LayoutedWord, float> scalingFunc) => WithScale(scalingFunc);
        public void SetColorFunction(Func<RenderingInfo, LayoutedWord, Color> colorFunc) => WithColor(colorFunc);

        public IEnumerable<LayoutedWord> SizeWords(IEnumerable<LayoutedWord> words)
        {
            var sizingInfo = new SizingInfo(this, words.ToArray());
            foreach (var word in sizingInfo.WordsArray)
            {
                var size = graphics.MeasureString(word.Word, DefaultFont);
                var scale = scaleFunction(sizingInfo, word);
                size = new SizeF(size.Width * scale, size.Height * scale);
                yield return new LayoutedWord(word.Word, word.Count, size);
            }
        }

        public virtual void Render(IEnumerable<LayoutedWord> words)
        {
            var renderingInfo = new RenderingInfo(this, words.ToArray());
            if (AutoSize)
                Output = new Bitmap(
                    (int) renderingInfo.WordsBorders.Size.Width,
                    (int) renderingInfo.WordsBorders.Size.Height);
            foreach (var word in renderingInfo.WordsArray)
            {
                var font = ScaledToRectangle(fontFunction(renderingInfo, word), word.Rectangle);
                var color = colorFunction(renderingInfo, word);
                var rectangle = word.Rectangle;

                if (!AutoSize) rectangle.Offset(Output.Width / 2f, Output.Height / 2f);
                else rectangle.Offset(-renderingInfo.WordsBorders.X, -renderingInfo.WordsBorders.Y);

                Render(word, font, color, rectangle, renderingInfo);
            }
        }

        protected virtual void Render(LayoutedWord word, Font font, Color color, RectangleF rectangle, RenderingInfo info)
        {
            graphics.DrawString(word.Word, font, new SolidBrush(color), rectangle.Location);
        }

        private static Font DefaultFontFunction(RenderingInfo info, LayoutedWord word) => info.Renderer.DefaultFont;

        private static Color DefaultColorFunction(RenderingInfo info, LayoutedWord word)
        {
            var minCount = info.MinWordCount;
            var maxCount = info.MaxWordCount;
            var t = (word.Count - minCount) / (float) (maxCount - minCount);
            return Color.FromArgb(
                info.Renderer.LerpInt(0, 255, t),
                info.Renderer.LerpInt(255, 0, t),
                128);
        }

        private Font ScaledToRectangle(Font font, RectangleF rectangle)
        {
            var scale = rectangle.Height / graphics.MeasureString("h", font).Height;
            return new Font(font.FontFamily, font.Size * scale, font.Style, font.Unit);
        }

        private int LerpInt(int a, int b, float t) => (int) (a + (b - a) * t);
        private float Lerp(float a, float b, float t) => a + (b - a) * t;
    }
}