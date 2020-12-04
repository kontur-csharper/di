using System;
using System.Drawing;
using System.IO;
using TagsCloud.WordLayouters;

namespace TagsCloud.CloudRenderers
{
    public class CloudRenderer : ICloudRenderer
    {
        private const string SamplesDirectory = "Samples";
        private readonly string path;
        private readonly IWordLayouter layouter;
        private readonly int width;
        private readonly int height;

        public CloudRenderer(IWordLayouter layouter, int width, int height)
        {
            if (layouter == null || width <= 0 || height <= 0) throw new ArgumentException();
            this.layouter = layouter;
            this.width = width;
            this.height = height;

            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent;
            if (directoryInfo == null) throw new DirectoryNotFoundException();
            path = $"{directoryInfo.FullName}\\{SamplesDirectory}";
            if(!Directory.Exists(path))
                directoryInfo.CreateSubdirectory(SamplesDirectory);
        }
        
        public string RenderCloud()
        {
            var wordsRectangle = layouter.GetCloudRectangle();
            var words = layouter.CloudWords;
            
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.ScaleTransform((float) width / wordsRectangle.Width, (float) height / wordsRectangle.Height);
            graphics.TranslateTransform(-wordsRectangle.X, -wordsRectangle.Y);
            foreach (var word in words)
            {
                graphics.DrawString(word.Value, word.Font, new SolidBrush(word.Color), word.Rectangle.Location);
            }
            
            graphics.Save();
            var imagePath = $"{path}\\sample.png";
            bitmap.Save(imagePath);
            return imagePath;
        }
    }
}