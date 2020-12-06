using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class WordsRendererToImageDebug : WordRendererToImage
    {
        public List<WordRenderingInfo> OutputInfo = new List<WordRenderingInfo>();
        public RenderingInfo RenderingInfo;

        protected override void Render(LayoutedWord word, Font font, Color color, RectangleF rectangle,
            RenderingInfo info)
        {
            RenderingInfo = info;
            OutputInfo.Add(new WordRenderingInfo(word, font, color, rectangle));
            base.Render(word, font, color, rectangle, info);
        }

        public class WordRenderingInfo
        {
            public LayoutedWord Word;
            public Font Font;
            public Color Color;
            public RectangleF Rectangle;

            public WordRenderingInfo(LayoutedWord word, Font font, Color color, RectangleF rectangle)
            {
                Word = word;
                Font = font;
                Color = color;
                Rectangle = rectangle;
            }
        }
    }
}