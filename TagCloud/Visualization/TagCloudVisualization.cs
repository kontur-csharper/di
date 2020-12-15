using System.Drawing;
using TagCloud.CloudLayoters;

namespace TagCloud.Visualization
{
    public static class TagCloudVisualization
    {
        internal static void Visualize(TagCloud cloud, string path, VisualizationInfo info)
        {
            var bitmap = info.TryGetSize(out var size) ? new Bitmap(size.Width, size.Height) : 
                new Bitmap(2 * cloud.layouter.Size().Width, 2 * cloud.layouter.Size().Height);
            var vectorShift = new Point(
                bitmap.Size.Width / 2 - cloud.Center.X, 
                bitmap.Size.Height / 2 - cloud.Center.Y);
            var graphics = Graphics.FromImage(bitmap);
            foreach (var location in cloud)
            {
                var color = info.GetColor(location.word, location.location, cloud);
                var pen = new Pen(color);
                graphics.DrawRectangle(pen, ShiftRectangle(location.location));
                graphics.DrawString(location.word, info.GetFont(location.location.Height), 
                    info.GetSolidBrush(location.word, location.location, cloud), 
                    ShiftRectangle(location.location));
            }
            bitmap.Save(path);

            Rectangle ShiftRectangle(Rectangle r) =>
                new Rectangle(r.X + vectorShift.X, r.Y + vectorShift.Y, r.Width, r.Height);
        }
    }
}
