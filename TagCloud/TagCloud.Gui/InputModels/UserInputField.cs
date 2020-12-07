using System.Windows.Forms;

namespace TagCloud.Gui.InputModels
{
    public class UserInputField
    {
        private TextBox? textBox;

        public UserInputField(string description)
        {
            Description = description;
        }

        public string Description { get; }

        public string Value
        {
            get => textBox?.Text ?? string.Empty;
            set
            {
                if (textBox != null)
                    textBox.Text = value;
            }
        }

        public void LinkTo(TextBox newTextBox) => textBox = newTextBox;
    }
}