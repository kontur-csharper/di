using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace CloudContainer
{
    public class TagCloudArguments
    {
        public TagCloudArguments()
        {
            Center = new Point(1500, 1500);
            ImageSize = new Size(1500, 1500);
            Font = new Font(FontFamily.GenericMonospace, 25);
            TextColor = Color.Blue;
            BoringWords = new List<string>
            {
                "в", "без", "до", "для", "за", "через", "над", "по", "из", "у", "около",
                "под", "о", "про", "на", "к", "перед", "при", "с", "между"
            }.ToHashSet();
            InputFileName = "text.txt";
            OutputFileName = Path.GetRandomFileName();
        }

        public Font Font { get; set; }
        public Size ImageSize { get; set; }
        public Point Center { get; set; }
        public Color TextColor { get; set; }

        public HashSet<string> BoringWords { get; set; }

        public string InputFileName { get; set; }
        public string OutputFileName { get; set; }
    }
}