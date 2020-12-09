using System.Collections.Immutable;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudContainer.Infrastructure.Settings
{
    public interface IFilteringWordsSettingsHolder
    {
        public ImmutableHashSet<GramPartsEnum> BoringGramParts { get; }
    }
}
