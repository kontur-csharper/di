using System.IO;
using Newtonsoft.Json;

namespace TagsCloudVisualization.Infrastructure.Common
{
    public static class SettingsSerializer
    {
        private const string PathToSettings = "appsettings.json";
        private static SettingsManager SettingsManager { get; set; }

        public static SettingsManager Deserialize()
        {
            SettingsManager = JsonConvert.DeserializeObject<SettingsManager>(File.ReadAllText(PathToSettings));
            return SettingsManager;
        }

        public static void Serialize() => File.WriteAllText(PathToSettings, JsonConvert.SerializeObject(SettingsManager));
    }
}