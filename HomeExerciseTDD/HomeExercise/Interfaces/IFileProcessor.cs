using System.Collections.Generic;

namespace HomeExercise
{
    public interface IFileProcessor
    {
        public Dictionary<string, int> GetWords();
    }
}