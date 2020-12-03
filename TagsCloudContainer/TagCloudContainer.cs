using TagsCloudContainer.Drawer;
using TagsCloudContainer.WordsParser;

namespace TagsCloudContainer
{
    public class TagCloudContainer : ITagCloudContainer
    {
        private readonly IRectangleLayout rectangleLayout;
        private readonly IWordsAnalyzer wordsAnalyzer;

        public TagCloudContainer(IWordsAnalyzer wordsAnalyzer, IRectangleLayout rectangleLayout)
        {
            this.wordsAnalyzer = wordsAnalyzer;
            this.rectangleLayout = rectangleLayout;
        }

        public void MakeTagCloud()
        {
            rectangleLayout.PlaceWords(wordsAnalyzer.AnalyzeWords());
            rectangleLayout.DrawLayout();
        }

        public void SaveTagCloud()
        {
            rectangleLayout.SaveLayout();
        }
    }
}