using System.Windows.Forms;
using TagsCloud.ClientGUI.Infrastructure;
using TagsCloud.Common;
using TagsCloud.Core;
using TagsCloud.FileReader;
using TagsCloud.Visualization;

namespace TagsCloud.ClientGUI
{
    public class TagsCloudPainter
    {
        private readonly PathSettings pathSettings;
        private readonly CloudVisualization visualizer;
        private readonly IReaderFactory readerFactory;

        public TagsCloudPainter(PictureBoxImageHolder pictureBox, PathSettings pathSettings,
            CloudVisualization visualizer, IReaderFactory readerFactory)
        {
            PictureBox = pictureBox;
            this.pathSettings = pathSettings;
            this.visualizer = visualizer;
            this.readerFactory = readerFactory;
        }

        public PictureBoxImageHolder PictureBox { get; }

        public void Paint(ICircularCloudLayouter cloud)
        {
            var words = TagsHelper.GetWords(pathSettings.PathToText, pathSettings.PathToBoringWords,
                pathSettings.PathToDictionary, pathSettings.PathToAffix, readerFactory);

            visualizer.Paint(cloud, words);

            PictureBox.Refresh();
            Application.DoEvents();
        }
    }
}