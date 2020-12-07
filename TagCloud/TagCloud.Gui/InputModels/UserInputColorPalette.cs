using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagCloud.Gui.InputModels
{
    public class UserInputColorPalette
    {
        public UserInputColorPalette(string description, IList<Color> initialValue)
        {
            Description = description;
            PickedColors = initialValue.ToList();
        }

        public string Description { get; }
        public List<Color> PickedColors { get; }

        public void AddColor(Color color)
        {
            if (!PickedColors.Contains(color))
                PickedColors.Add(color);
        }

        public void RemoveColor(Color color)
        {
            if (PickedColors.Contains(color))
                PickedColors.Remove(color);
        }
    }
}