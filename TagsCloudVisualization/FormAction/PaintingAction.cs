using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.FormAction
{
    public class PaletteAction : IFormAction
    {
        public string Category => "Settings";
        public string Name => "Painting";
        public string Description => "Change colors of your cloud visualization";
        
        private readonly PaintingSettings paintingSettings;
        
        public PaletteAction(PaintingSettings paintingSettings)
        {
            this.paintingSettings = paintingSettings;
        }
        
        public void Perform()
        {
            SettingsForm.For(paintingSettings).ShowDialog();
        }
    }
}