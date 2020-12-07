using System.ComponentModel;

namespace TagsCloud.WordFilters
{
    public enum PartsOfSpeech
    {
        [Description("Прилагательное")] 
        A,

        [Description("Наречие")] 
        ADV,

        [Description("Местоименное наречие")] 
        ADVPRO,

        [Description("Числительное - прилагательное")]
        ANUM,

        [Description("Местоимение - прилагательное")]
        APRO,

        [Description("Часть композита - сложного слова")]
        COM,

        [Description("Союз")] 
        CONJ,

        [Description("Междометие")]
        INTJ,

        [Description("Числительное")]
        NUM,

        [Description("Частица")] 
        PART,

        [Description("Предлог")]
        PR,

        [Description("Существительное")]
        S,

        [Description("Местоимение-существительное")]
        SPRO,

        [Description("Глагол")]
        V
    }
}