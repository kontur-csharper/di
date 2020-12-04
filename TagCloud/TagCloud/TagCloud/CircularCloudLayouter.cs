using System.Drawing;
using System.Linq;
using TagCloud.Curves;
using TagCloud.WordsFilter;
using TagCloud.WordsProvider;

namespace TagCloud
{
    public class CircularCloudLayouter : TagCloud
    {
        private readonly ICurve curve;

        public CircularCloudLayouter(
            ICurve curve,
            IWordsProvider wordsProvider,
            IWordsFilter wordsFilter)
        {
            this.curve = curve;
            WordsProvider = wordsProvider;
            WordsFilter = wordsFilter;
        }

        protected override IWordsProvider WordsProvider { get; }
        protected override IWordsFilter WordsFilter { get; }

        public override WordRectangle PutNextWord(string word, Size rectangleSize)
        {
            while (true)
            {
                var currentPoint = curve.CurrentPoint;
                var possibleRectangle = new Rectangle(currentPoint, rectangleSize);
                var canFit = WordRectangles.All(rect => !rect.Rectangle.IntersectsWith(possibleRectangle));
                curve.Next();
                if (!canFit) continue;
                var wordRectangle = new WordRectangle(possibleRectangle, word);
                WordRectangles.Add(wordRectangle);
                return wordRectangle;
            }
        }
    }
}