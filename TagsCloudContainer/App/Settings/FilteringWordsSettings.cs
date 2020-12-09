using System.Collections.Immutable;
using TagsCloudContainer.Infrastructure.Settings;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudContainer.App.Settings
{
    public class FilteringWordsSettings : IFilteringWordsSettingsHolder
    {
        public static readonly FilteringWordsSettings Instance = new FilteringWordsSettings();

        private FilteringWordsSettings()
        {
            BoringGramParts = ImmutableHashSet.Create(GramPartsEnum.Interjection,
                GramPartsEnum.NounPronoun, GramPartsEnum.PronounAdjective,
                GramPartsEnum.Conjunction, GramPartsEnum.Pretext);
        }

        public ImmutableHashSet<GramPartsEnum> BoringGramParts { get; set; } 
    }
}
