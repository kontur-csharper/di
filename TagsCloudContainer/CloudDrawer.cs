using System.Collections.Generic;
using System.Drawing;
using System;

namespace TagsCloudContainer
{
    class CloudDrawer : ICloudDrawer
    {
        private ICloudLayouter cloudLayouter;
        public IImageSaver ImageSaver { get; set; }
        public IColorProvider ColorProvider { get; set; }
        private int ImageSize { get; set; }

        public CloudDrawer(ICloudLayouter cloudLayouter, IColorProvider colorProvider, IImageSaver imageSaver)
        {
            this.cloudLayouter = cloudLayouter;
            ChangeImageSize(300);
            cloudLayouter.ChangeCenter(new Point(ImageSize / 2, ImageSize / 2));
            ImageSaver = imageSaver;
            ColorProvider = colorProvider;
        }

        public void DrawCloud(List<WordWithFont> words, string targetPath, string imageName)
        {
            using var bitmap = new Bitmap(ImageSize, ImageSize);
            using var graphics = Graphics.FromImage(bitmap);
            var layout = MakeLayout(words, graphics); 
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, ImageSize, ImageSize));
            for (var i = 0; i < words.Count; i++)
            {
                graphics.DrawString(words[i].Word, words[i].Font, new SolidBrush(ColorProvider.GetNextColor()),
                    layout[i].Location);
            }

            ImageSaver.Save(targetPath, imageName, bitmap);
        }

        private List<Rectangle> MakeLayout(IEnumerable<WordWithFont> words, Graphics graphics)
        {
            cloudLayouter.Reset();
            cloudLayouter.ChangeCenter(new Point(ImageSize / 2, ImageSize / 2));
            foreach (var word in words)
            {
                var wordSize = graphics.MeasureString(word.Word, word.Font);
                cloudLayouter.PutNextRectangle(wordSize.ToSize());
            }

            return (List<Rectangle>)cloudLayouter.Rectangles;
        }

        public void ChangeImageSize(int newSize)
        {
            ImageSize = newSize;
        }
    }
}
