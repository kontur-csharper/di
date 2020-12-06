using System.Drawing;

namespace TagsCloudVisualization.AppSettings
{
    public class FontSettings
    {
        public Font Font { get; set; } = new Font("Times New Roman", 14);
        public float MinSize { get; set; } = 7;
        public float MaxSize { get; set; } = 55;
    }
}