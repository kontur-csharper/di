using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using TagCloud.Core.Layouting;
using TagCloud.Core.Text.Formatting;

namespace TagCloud.Core
{
    public interface ITagCloudGenerator : IDisposable
    {
        Task<Image> DrawWordsAsync(
            FontSizeSourceType sizeSourceType,
            LayouterType layouterType,
            Color[] palette,
            Dictionary<string, int> wordsCollection,
            FontFamily fontFamily,
            Point centerPoint,
            Size betweenRectanglesDistance,
            CancellationToken token);
    }
}