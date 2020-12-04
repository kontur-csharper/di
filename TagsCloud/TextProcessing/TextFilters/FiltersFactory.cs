using System.Linq;
using TagsCloud.Factory;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloud.TextProcessing.TextFilters
{
    public class FiltersFactory : ServiceFactory<ITextFilter>
    {
        private readonly WordConfig wordsConfig;

        public FiltersFactory(WordConfig wordsConfig)
        {
            this.wordsConfig = wordsConfig;
        }

        public override ITextFilter Create()
        {
            var filterNames = wordsConfig.FilersNames;
            return new CompositeFilter(filterNames.Select(name => services[name]()).ToArray());
        }

        private class CompositeFilter : ITextFilter
        {
            private readonly ITextFilter[] filters;

            public CompositeFilter(ITextFilter[] filters)
            {
                this.filters = filters;
            }

            public bool CanTake(string word) => filters.All(filter => filter.CanTake(word));
        }
    }
}
