using TagsCloudVisualization.AppSettings;

namespace TagsCloudVisualization.FormAction
{
    public class ForbiddenWordsAction : IFormAction
    {
        public string Category { get; } = "Configuration";
        public string Name { get; } = "Forbidden words";
        public string Description { get; } = "Select words that shouldn't appear in the tag cloud";

        private readonly WordsSettings wordsSettings;
        
        public ForbiddenWordsAction(WordsSettings wordsSettings)
        {
            this.wordsSettings = wordsSettings;
        }

        public void Perform()
        {
            SettingsForm.For(wordsSettings).ShowDialog();
        }
    }
}