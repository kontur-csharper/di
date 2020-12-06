using System;
using System.Windows.Forms;
using FractalPainting.Infrastructure.Common;

namespace FractalPainting.App
{
    public class SettingsManager
    {
        private readonly IObjectSerializer serializer;
        private readonly IBlobStorage storage;
        private string settingsFilename;

        public SettingsManager(IObjectSerializer serializer, IBlobStorage storage)
        {
            this.serializer = serializer;
            this.storage = storage;
        }

        public AppSettings Load()
        {
            try
            {
                settingsFilename = "app.settings";
                var data = storage.Get(settingsFilename);
                if (data == null)
                {
                    var defaultSettings = CreateDefaultSettings();
                    Save(defaultSettings);
                    return defaultSettings;
                }

                return serializer.Deserialize<AppSettings>(data);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "не удалось загрузить настройки");
                return CreateDefaultSettings();
            }
        }

        private static AppSettings CreateDefaultSettings()
        {
            return new AppSettings
            {
                ImagesDirectory = ".",
                ImageSettings = new ImageSettings()
            };
        }

        public void Save(AppSettings settings)
        {
            storage.Set(settingsFilename, serializer.Serialize(settings));
        }
    }
}