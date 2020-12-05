using System.Drawing;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudUI
{
    public class FormConfig
    {
        public FormConfig(Color backgroundColor, Size formSize, SpiralType spiralType)
        {
            BackgroundColor = backgroundColor;
            FormSize = formSize;
            SpiralType = spiralType;
        }

        public Color BackgroundColor { get; }
        public Size FormSize { get; }
        public SpiralType SpiralType { get; }
    }
}