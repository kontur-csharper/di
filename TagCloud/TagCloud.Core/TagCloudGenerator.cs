using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TagCloud.Core.Layouting;
using TagCloud.Core.Text.Formatting;
using TagCloud.Core.Utils;
using TagCloud.Core.Visualisation;

namespace TagCloud.Core
{
    public sealed class TagCloudGenerator : ITagCloudGenerator
    {
        private readonly Graphics stubGraphics;

        private readonly ILayouterResolver layouterResolver;
        private readonly IFontSizeSourceResolver sizeSourceResolver;

        public TagCloudGenerator(ILayouterResolver layouterResolver, IFontSizeSourceResolver sizeSourceResolver)
        {
            this.layouterResolver = layouterResolver;
            this.sizeSourceResolver = sizeSourceResolver;

            stubGraphics = Graphics.FromHwnd(IntPtr.Zero);
        }

        public async Task<Image> DrawWordsAsync(
            FontSizeSourceType sizeSourceType,
            LayouterType layouterType,
            Color[] palette,
            Dictionary<string, int> wordsCollection,
            FontFamily fontFamily,
            Point centerPoint,
            Size betweenRectanglesDistance,
            CancellationToken token)
        {
            if (wordsCollection.Count == 0)
                throw new ArgumentException($"{nameof(wordsCollection)} is empty", nameof(wordsCollection));
            var fontsCollection = sizeSourceResolver.Get(sizeSourceType).GetFontSizesForAll(wordsCollection);
            var layouter = layouterResolver.Get(layouterType);

            return await Task.Run(() =>
            {
                var formattedWords = wordsCollection
                    .OrderByDescending(x => x.Value)
                    .Select(word => new {Word = word.Key, FontSize = fontsCollection[word.Key]})
                    .Select(x => FormattedWordFrom(x.Word, Randomized.ItemFrom(palette), fontFamily, x.FontSize))
                    .ToDictionary(fw => fw.Word);

                var wordSizesEnumerable = formattedWords.Select(x =>
                    Size.Ceiling(stubGraphics.MeasureString(x.Value.Word, x.Value.Font))
                );

                var putWords = layouter.PutAll(centerPoint, betweenRectanglesDistance, wordSizesEnumerable);

                using var cloudVisualiser = new CloudVisualiser();
                foreach (var (formattedWord, placedWord) in formattedWords.Values.Zip(putWords))
                {
                    cloudVisualiser.DrawNextWord(placedWord, formattedWord);

                    formattedWord.Dispose();
                    if (token.IsCancellationRequested)
                        break;
                }

                return (Image) cloudVisualiser.Current!.Clone();
            }, token).ConfigureAwait(false);
        }

        private static FormattedWord FormattedWordFrom(string word, Color color, FontFamily fontFamily,
            float wordSize)
        {
            return new FormattedWord(word,
                new Font(fontFamily, wordSize),
                new SolidBrush(color));
        }

        public void Dispose()
        {
            stubGraphics.Dispose();
        }
    }
}