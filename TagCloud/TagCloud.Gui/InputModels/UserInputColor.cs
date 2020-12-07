using System.Drawing;

namespace TagCloud.Gui.InputModels
{
    public class UserInputColor
    {
        public UserInputColor(string description, Color defaultValue)
        {
            Description = description;
            Picked = defaultValue;
        }

        public string Description { get; }
        public Color Picked { get; set; }
    }
}