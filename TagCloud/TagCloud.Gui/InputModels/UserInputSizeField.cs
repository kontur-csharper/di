using System.Drawing;

namespace TagCloud.Gui.InputModels
{
    public class UserInputSizeField
    {
        public UserInputSizeField(string description, bool showAsPoint)
        {
            Description = description;
            ShowAsPoint = showAsPoint;
        }

        public string Description { get; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool ShowAsPoint { get; }
        
        public Point PointFromCurrent() => new Point(Width, Height);
        public Size SizeFromCurrent() => new Size(Width, Height);
    }
}