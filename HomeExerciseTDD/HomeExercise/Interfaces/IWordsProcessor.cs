using System.Collections.Generic;

namespace HomeExercise
{
    public interface IWordsProcessor
    {
        public List<IWord> HandleWords(); 
    }
}