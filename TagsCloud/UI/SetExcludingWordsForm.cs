using System;
using System.Windows.Forms;
using TagsCloud.App;

namespace TagsCloud.UI
{
    public partial class SetExcludingWordsForm : Form
    {
        private readonly BlackListWordsFilter filter;
        private readonly TextBox textBox;

        public SetExcludingWordsForm(BlackListWordsFilter filter)
        {
            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom
            };
            textBox = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                Text = string.Join(Environment.NewLine, filter.BlackList) + Environment.NewLine
            };
            this.filter = filter;
            okButton.Click += OnOkButtonClick;
            Controls.Add(okButton);
            Controls.Add(textBox);
        }

        private void OnOkButtonClick(object sender, EventArgs e)
        {
            filter.UpdateBlackList(textBox.Text.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}