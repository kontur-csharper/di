using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagCloudPainter
    {
        private readonly IImageHolder imageHolder;
        private readonly ImageSettings imageSettings;

        public TagCloudPainter(IImageHolder imageHolder, ImageSettings imageSettings)
        {
            this.imageHolder = imageHolder;
            this.imageSettings = imageSettings;
        }

        public Result<None> Paint(IEnumerable<Word> words)
        {
            var imageSize = imageSettings.ImageSize;
            imageHolder.RecreateImage(imageSize); 
            var graphics = imageHolder.StartDrawing();
            return Result
                .Of(() => words.Select(x => ValidateDrawingWord(x, imageSize)))
                .Then(wordsToDraw =>
                {
                    foreach (var word in wordsToDraw)
                    {
                        graphics.DrawString(word.Text, word.Font, new SolidBrush(imageSettings.GetColor()),
                            word.Rectangle.Location);
                    }
                });
        }

        private Word ValidateDrawingWord(Word word, ImageSize imageSize)
        {
            if (!word.Rectangle.IsNestedInImage(imageSize))
                throw new InvalidOperationException("Image is too small for tag cloud. Please set more image size");
            return word;
        }
    }
}