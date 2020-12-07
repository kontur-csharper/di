namespace MyStem.Wrapper.Workers.Grammar.Parsing.Models
{
    public enum MyStemSpeechPart
    {
        Unrecognized = 0,

        ///<summary>Прилагательное</summary> 
        Adjective = 1,

        ///<summary>Наречие</summary> 
        Adverb = 2,

        ///<summary>Местоименное наречие</summary> 
        PronominalAdverb = 3,

        ///<summary>Числительное-прилагательное</summary> 
        PronounNumeral = 4,

        ///<summary>Местоимение-прилагательное</summary> 
        PronounAdjective = 5,

        ///<summary>Часть композита - сложного слова</summary> 
        CompositeWordPart = 6,

        ///<summary>Союз</summary> 
        Union = 7,

        ///<summary>Междометие</summary> 
        Interjection = 8,

        ///<summary>Числительное</summary> 
        Numeral = 9,

        ///<summary>Частица</summary> 
        Particle = 10,

        ///<summary>Предлог</summary> 
        Pretext = 11,

        ///<summary>Существительное</summary> 
        Noun = 12,

        ///<summary>Местоимение-существительное</summary> 
        Pronoun = 13,

        ///<summary>Глагол</summary>
        Verb = 14,
    }
}