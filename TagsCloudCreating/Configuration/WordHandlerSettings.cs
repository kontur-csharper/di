using System.Collections.Generic;

namespace TagsCloudCreating.Configuration
{
    public class WordHandlerSettings
    {
        public Dictionary<string, bool> SpeechPartsStatuses { get; set; } = new Dictionary<string, bool>
        {
            ["Прилагательное"] = true,
            ["Наречие"] = false,
            ["Местоимение-наречие"] = false,
            ["Числительное-прилагательное"] = true,
            ["Местоимение-прилагательное"] = false,
            ["Часть сложного слова"] = true,
            ["Союз"] = false,
            ["Междометие"] = true,
            ["Числительное"] = true,
            ["Частица"] = false,
            ["Предлог"] = false,
            ["Существительное"] = true,
            ["Местоимение-существительное"] = false,
            ["Глагол"] = true
        };
    }
}