using System.IO;
using TagsCloudContainer.Infrastructure.Settings;

namespace TagsCloudContainer.App.Settings
{
    public class InputSettings : IInputSettingsHolder
    {
        public static readonly InputSettings Instance = new InputSettings();

        private InputSettings()
        {
            InputFileName = Path.Combine(Directory.GetCurrentDirectory(),
                "..", "..", "..", "text.txt");
        }

        public string InputFileName { get; set; }
    }
}