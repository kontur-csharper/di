using System.Drawing;

namespace TagCloud.Settings
{
    public class CloudSettings
    {
        public Point Center { get; set; }
        public double StartRadius { get; set; }
        public Color InnerColor { get; set; }
        public Color OuterColor { get; set; }
        public double InnerColorRadius { get; set; }
        public double OuterColorRadius { get; set; }
        public Font Font { get; set; }

        public static CloudSettings GetDefault()
        {
            return new CloudSettings
            {
                Center = new Point(0, 0),
                StartRadius = 0,
                InnerColor = Color.Coral,
                OuterColor = Color.CornflowerBlue,
                InnerColorRadius = 0,
                OuterColorRadius = 4000,
                Font = new Font(FontFamily.GenericMonospace, 24)
            };
        }
    }
}
