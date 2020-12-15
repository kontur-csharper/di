using System.Drawing;
using System.Linq;
using TagCloud.Visualization.WordsColorings;

namespace TagCloud.Visualization
{
    internal class VisualizationInfo
    {
        private readonly Size? size;
        private readonly string font;
        private readonly IWordsColoring wordsColoring;

        internal VisualizationInfo(IWordsColoring coloring, Size? size = null, string font = null)
        {
            this.size = size;
            this.font = Fonts.GetFont(font);
            wordsColoring = coloring;
        }

        internal static Size? ReadSize(string sizeStr)
        {
            try
            {
                var result = ParseString(sizeStr);
                if (result.Length != 2)
                    return null;
                if (result.Any(i => i < 0))
                    return null;
                return new Size(result[0], result[1]);
            }
            catch
            {
                return null;
            }
        }

        private static int[] ParseString(string str) =>
            str.Split(' ')
            .Where(s => s != string.Empty)
            .Select(s => int.Parse(s))
            .ToArray();

        internal bool TryGetSize(out Size size)
        {
            size = this.size ?? Size.Empty;
            return this.size != null;
        }

        internal Font GetFont(int emSize) => new Font(font, emSize);

        internal Color GetColor(string word, Rectangle location, TagCloud cloud) =>
            wordsColoring.GetColor(word, location, cloud);
        
        internal SolidBrush GetSolidBrush(string word, Rectangle location, TagCloud cloud) => 
            new SolidBrush(GetColor(word, location, cloud));
    }
}
