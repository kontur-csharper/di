using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudCreating.Configuration;
using TagsCloudVisualization.Contracts;

namespace TagsCloudVisualization.MenuItems.Settings
{
    public class BoringWordsSelector : Form, IMenuItem
    {
        private WordHandlerSettings Settings { get; }
        public string MenuAffiliation => "Settings";
        public string ItemName => "Boring words...";
        private CheckBox[] SpeechPartsList { get; }

        private readonly TableLayoutPanel table = new TableLayoutPanel
        {
            AutoSize = true,
            Padding = new Padding(40, 20, 0, 20),
            Dock = DockStyle.Fill,
            ColumnCount = 1
        };

        public BoringWordsSelector(WordHandlerSettings settings)
        {
            Settings = settings;
            table.RowCount = settings.SpeechPartsStatuses.Count;

            var acceptButton = new Button {Text = "OK", DialogResult = DialogResult.OK};
            var header = new Label
            {
                Text = "What parts of speech will be in the cloud?",
                Font = new Font(SystemFonts.CaptionFont.FontFamily, 9, FontStyle.Bold),
                AutoSize = true
            };
            SpeechPartsList = ConvertToSpeechList(settings.SpeechPartsStatuses);

            Controls.Add(header);
            FillSpeechPartsList(SpeechPartsList);
            table.Controls.Add(acceptButton, 0, SpeechPartsList.Length + 1);

            CenterElements(header, acceptButton);

            Controls.Add(table);
            AcceptButton = acceptButton;
        }

        private void CenterElements(Label header, Button acceptButton)
        {
            header.Padding = new Padding(GetLeftPadding(ClientSize, header.Size), 15, 0, 0);
            table.Padding = new Padding(GetLeftPadding(ClientSize, table.Size), 40, 0, 10);
            acceptButton.Margin = new Padding(GetLeftPadding(table.Size, acceptButton.Size), 10, 0, 10);
        }

        public DialogResult Execute() => ShowDialog();

        private int GetLeftPadding(Size parent, Size child) => parent.Width / 2 - child.Width / 2;

        private void FillSpeechPartsList(CheckBox[] checkBoxes)
        {
            for (var i = 0; i < checkBoxes.Length; i++)
                table.Controls.Add(checkBoxes[i], 0, i + 1);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AutoSize = true;
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private static CheckBox[] ConvertToSpeechList(Dictionary<string, bool> speechPartStatuses)
        {
            var checkBoxes = new List<CheckBox>();
            foreach (var (speechPart, status) in speechPartStatuses)
            {
                var checkBox = ConvertToCheckBox(speechPart, status);
                checkBox.Click += (sender, args) => speechPartStatuses[speechPart] = !speechPartStatuses[speechPart];
                checkBoxes.Add(checkBox);
            }

            return checkBoxes.ToArray();
        }

        private static CheckBox ConvertToCheckBox(string speechPart, bool status) =>
            new CheckBox {Text = speechPart, Checked = status, AutoSize = true};
    }
}