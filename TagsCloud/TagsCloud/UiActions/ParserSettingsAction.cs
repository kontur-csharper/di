using System;
using System.Collections.Generic;
using TagsCloud.GUI;

namespace TagsCloud.UiActions
{
    class ParserSettingsAction : IUiAction
    {
        public string Category => "Настройки";
        public string Name => "Настройка отбора слов";
        public string Description => "Настройка отбора слов";
        private HashSet<string> wordsToIgnore;

        public ParserSettingsAction(HashSet<string> wordsToIgnore)
        {
            this.wordsToIgnore = wordsToIgnore;
        }
        public void Perform()
        {
            var dialog = new ParserSettingsForm(wordsToIgnore);
            dialog.Show();
        }
    }
}
