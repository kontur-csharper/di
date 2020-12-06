using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer
{
    public interface IParameterizedWordRendererToImage : IWordRenderer
    {
        void SetFontFunction(Func<RenderingInfo, LayoutedWord, Font> fontFunc);
        void SetScalingFunction(Func<SizingInfo, LayoutedWord, float> scalingFunc);
        void SetColorFunction(Func<RenderingInfo, LayoutedWord, Color> colorFunc);
        Image Output { get; set; }
        bool AutoSize { get; set; }
    }

    public class SizingInfo
    {
        public readonly WordRendererToImage Renderer;
        public readonly LayoutedWord[] WordsArray;
        public readonly int MinWordCount;
        public readonly int MaxWordCount;
        public readonly int TotalWordsCount;

        public SizingInfo(WordRendererToImage renderer, LayoutedWord[] wordsArray)
        {
            Renderer = renderer;
            WordsArray = wordsArray;

            MinWordCount = wordsArray.Min(word => word.Count);
            MaxWordCount = wordsArray.Max(word => word.Count);
            TotalWordsCount = wordsArray.Sum(word => word.Count);
        }
    }

    public class RenderingInfo : SizingInfo
    {
        public RectangleF WordsBorders;

        public RenderingInfo(WordRendererToImage renderer, LayoutedWord[] wordsArray) : base(renderer, wordsArray)
        {
            WordsBorders = RectangleF.FromLTRB(
                WordsArray.Min(w => w.Rectangle.Left),
                WordsArray.Min(w => w.Rectangle.Top),
                WordsArray.Max(w => w.Rectangle.Right),
                WordsArray.Max(w => w.Rectangle.Bottom));
        }
    }
}