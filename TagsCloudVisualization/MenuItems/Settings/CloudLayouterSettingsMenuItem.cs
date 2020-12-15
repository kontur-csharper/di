using System.Windows.Forms;
using TagsCloudCreating.Configuration;
using TagsCloudVisualization.Contracts;
using TagsCloudVisualization.Infrastructure.Common;

namespace TagsCloudVisualization.MenuItems.Settings
{
    public class CloudLayouterSettingsMenuItem : IMenuItem
    {
        private CloudLayouterSettings LayouterSettings { get; }
        public string MenuAffiliation => "Settings";
        public string ItemName => "Cloud layouter settings...";

        public CloudLayouterSettingsMenuItem(CloudLayouterSettings layouterSettings) => LayouterSettings = layouterSettings;

        public DialogResult Execute() => SettingsForm.For(LayouterSettings).ShowDialog();
    }
}