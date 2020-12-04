using TagCloud.Drawers;
using TagCloud.TextReaders;
using TagCloud.WordsAnalyzer;
using TagCloud.ImageSavers;

namespace TagCloud
{
    public class TagCloud
    {
        private ITagDrawer drawer;
        private ITextReader textReader;
        private IWordsAnalyzer wordsAnalyzer;
        private IImageSaver imageSaver;
        
        public TagCloud(ITagDrawer drawer, ITextReader textReader, IWordsAnalyzer wordsAnalyzer, IImageSaver imageSaver)
        {
            this.drawer = drawer;
            this.textReader = textReader;
            this.wordsAnalyzer = wordsAnalyzer;
            this.imageSaver = imageSaver;
        }

        public void MakeTagCloud()
        {
            var words = textReader.ReadWords();
            var tags = wordsAnalyzer.GetTags(words);
            var result = drawer.DrawTagCloud(tags);
            imageSaver.Save(result);
        }
    }
}