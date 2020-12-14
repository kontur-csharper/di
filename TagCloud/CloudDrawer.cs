using System.Collections.Generic;
using System.Drawing;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class CloudDrawer : ICloudDrawer
    {
        private readonly Size pictureSize;

        public CloudDrawer(Size pictureSize)
        {
            this.pictureSize = pictureSize;
        }

        public Bitmap DrawCloud(IEnumerable<WordForCloud> wordsForCloud)
        {
            var bitmap = new Bitmap(pictureSize.Width, pictureSize.Height);
            var gr = Graphics.FromImage(bitmap);
            gr.FillRectangle(Brushes.White, 0, 0, pictureSize.Width, pictureSize.Height);
            foreach (var wordForCloud in wordsForCloud)
            {
                gr.DrawString($"{wordForCloud.Word}", wordForCloud.Font,
                              new SolidBrush(wordForCloud.WordColor), wordForCloud.WordSize);
            }

            return bitmap;
        }
    }
}