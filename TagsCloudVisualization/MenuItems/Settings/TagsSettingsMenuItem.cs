using System.Collections.Generic;
using System.Windows.Forms;
using TagsCloudCreating.Configuration;
using TagsCloudCreating.Contracts;
using TagsCloudVisualization.Contracts;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudVisualization.MenuItems.Settings
{
    public class TagsSettingsMenuItem : IMenuItem
    {
        private TagsSettings TagsSettings { get; } 
        private TagsSettingsSelector TagsSettingsSelector { get; }

        public TagsSettingsMenuItem(TagsSettings tagsSettings, IEnumerable<IColorizer> allColorizers)
        {
            TagsSettings = tagsSettings;
            TagsSettingsSelector = new TagsSettingsSelector(allColorizers);
        }

        public string MenuAffiliation => "Settings";
        public string ItemName => "Tag's configuration...";
        public DialogResult Execute()
        {
            var dialogResult = SettingsForm.For(TagsSettingsSelector).ShowDialog();
            if (dialogResult is DialogResult.OK)
                UpdateTagsSettings();
            return dialogResult;
        }

        private void UpdateTagsSettings()
        {
            TagsSettings.Colorizer = TagsSettingsSelector.SelectedColorizer;
            TagsSettings.Font = TagsSettingsSelector.SelectedFont;
        }
    }
}