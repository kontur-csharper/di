using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloud.Infrastructure.Text;
using TagCloud.Infrastructure.Text.Conveyors;
using TagCloud.Infrastructure.Text.Information;

namespace TagCloud.Infrastructure.Graphics
{
    public class TagCloudGenerator : IImageGenerator
    {
        private readonly IReader reader;
        private readonly IEnumerable<IConveyor> filters;
        private readonly IPainter painter;

        public TagCloudGenerator(IReader reader, IEnumerable<IConveyor> filters, IPainter painter)
        {
            this.reader = reader;
            this.filters = filters;
            this.painter = painter;
        }

        public Image Generate()
        {
            var tokens = reader.ReadTokens();
            var analyzedTokens = filters.Aggregate(
                tokens.Select(line => (line, new TokenInfo())),
                (current, filter) => filter.Handle(current).ToArray());
            return painter.GetImage(analyzedTokens);
        }
    }
}