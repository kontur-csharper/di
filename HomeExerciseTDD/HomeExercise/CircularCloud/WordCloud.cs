using System;
using System.Collections.Generic;
using System.Drawing;
using HomeExercise.Helpers;

namespace HomeExercise
{
    public class WordCloud : IWordCloud
    {
        private readonly IWordsProcessor wordsProcessor;
        public List<ISizedWord> SizedWords { get; }
        private ICircularCloudLayouter layouter;
        public Point Center { get;}

        public WordCloud(ICircularCloudLayouter layouter,  IWordsProcessor wordsProcessor)
        {
            Center = layouter.Center;
            SizedWords = new List<ISizedWord>();
            this.layouter = layouter;
            this.wordsProcessor = wordsProcessor;
        }

       public void BuildCloud()
       {
           var words = wordsProcessor.HandleWords();
           foreach (var word in words)
            {
                var rectangle = GetRectangle(word, word.Size);
                Console.WriteLine($"Height:{rectangle.Size.Height}{Environment.NewLine}Width:{rectangle.Size.Width}");
                SizedWords.Add(new SizedWord(word, word.Size, word.Font, rectangle));
            }
       }

        private Rectangle GetRectangle(IWord word, int size)
        {
            return layouter
                .PutNextRectangle(GraphicsHelper.MeasureString(word.Text, new Font(word.Font, size)));
        }
    }
}