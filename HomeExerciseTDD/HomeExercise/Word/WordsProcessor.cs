using System.Collections.Generic;
using System.Linq;
using HomeExercise.settings;

namespace HomeExercise
{
    public class WordsProcessor : IWordsProcessor
    {
        private readonly IFileProcessor fileProcessor;
        private readonly WordSettings settings;
        public WordsProcessor(IFileProcessor fileProcessor, WordSettings settings)
        {
            this.fileProcessor = fileProcessor;
            this.settings = settings;
        }
        
        public  List<IWord> HandleWords()
        {
            var words = fileProcessor.GetWords();
            return words
                .Select(w => WordHandle(w.Key, w.Value))
                .ToList();
        }
   
        private IWord WordHandle(string text, int frequency)
        {
            return new Word(text, frequency, settings.Font, frequency*settings.Coefficient);
        }
    }
}