using System;
using System.Drawing;
using System.Linq;
using TagCloud.Core.Text.Formatting;
using TagCloud.Core.Utils;

namespace TagCloud.Core.Visualisation
{
    public class CloudVisualiser : IDisposable
    {
        private bool disposed;
        private Graphics? graphics;
        private Image? image;

        public Image? Current => image;

        public void DrawNextWord(Rectangle position, FormattedWord formattedWord)
        {
            ThrowIfDisposed();
            var wordPosition = GetWordRectangle(graphics, formattedWord, position);
            var resized = EnsureBitmapSize(image, Rectangle.Ceiling(wordPosition));
            SetCurrentImage(resized);
            wordPosition.Location += image!.Size / 2;
            graphics!.DrawString(formattedWord.Word, formattedWord.Font, formattedWord.Brush, wordPosition);
        }

        private void SetCurrentImage(Image newImage)
        {
            if (newImage == image) return;

            image?.Dispose();
            graphics?.Dispose();
            image = newImage;
            graphics = Graphics.FromImage(newImage);
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
                throw new InvalidOperationException($"{nameof(CloudVisualiser)} is already disposed");
        }

        private static RectangleF GetWordRectangle(Graphics? graphics, FormattedWord toDraw, Rectangle position)
        {
            var wordSize = MeasureString(graphics, toDraw.Word, toDraw.Font);
            if (wordSize.Height > position.Size.Height || wordSize.Width > position.Size.Width)
                throw new ArgumentException("Actual word size is larger than computed values");

            var offset = (wordSize - position.Size) / 2;
            var wordPosition = new PointF(position.X - offset.Width, position.Y - offset.Height);
            return new RectangleF(wordPosition, wordSize);
        }

        private static SizeF MeasureString(Graphics? graphics, string? word, Font font)
        {
            if (graphics != null)
                return graphics.MeasureString(word, font);
            using var g = Graphics.FromHwnd(IntPtr.Zero);
            return g.MeasureString(word, font);
        }


        private static Image EnsureBitmapSize(Image? bitmap, Rectangle nextRectangle)
        {
            if (bitmap == null)
            {
                var xSize = MaxAbs(nextRectangle.Right, nextRectangle.Left) + nextRectangle.Width;
                var ySize = MaxAbs(nextRectangle.Top, nextRectangle.Bottom) + nextRectangle.Height;
                bitmap = new Bitmap(xSize, ySize);
            }
            else
            {
                var halfSize = bitmap.Size / 2;
                var xMaxDistance = MaxAbs(nextRectangle.Left, nextRectangle.Right, halfSize.Width);
                var yMaxDistance = MaxAbs(nextRectangle.Top, nextRectangle.Bottom, halfSize.Height);

                if (halfSize.Width != xMaxDistance || halfSize.Height != yMaxDistance)
                    return GraphicsUtils.PlaceAtCenter(bitmap, new Size(xMaxDistance * 2, yMaxDistance * 2));
            }

            return bitmap;
        }

        private static int MaxAbs(params int[] numbers) => numbers.Select(Math.Abs).Max();

        public void Dispose()
        {
            disposed = true;
            graphics?.Dispose();
            image?.Dispose();
        }
    }
}