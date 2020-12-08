using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TagsCloud.GUI
{
    class ParserSettingsForm : Form
    {
        public ParserSettingsForm(HashSet<string> wordsToIgnore)
        {
            Text = "Настройка отбора слов";
            Size = new Size(300, 350);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            var ignoredWords = new ListBox {Height = 250, Width = Size.Width};
            Shown += (sender, args) =>
            {
                ignoredWords.Items.Clear();
                ignoredWords.Items.AddRange(wordsToIgnore?.ToArray());
            };

            var wordToExclude = new TextBox{Dock = DockStyle.Bottom};

            var addButton = new Button {Text = "Добавить слово для исключения",Dock = DockStyle.Bottom};
            addButton.Click += (sender, args) =>
            {
                if(string.IsNullOrEmpty(wordToExclude.Text))
                    return;

                if (!wordsToIgnore.Contains(wordToExclude.Text))
                {
                    ignoredWords.Items.Add(wordToExclude.Text.ToLower());
                    wordsToIgnore.Add(wordToExclude.Text.ToLower());
                }
                else
                    MessageBox.Show("Данное слово уже исключено");
                wordToExclude.Text = string.Empty;
            };

            var deleteButton = new Button {Text = "Удалить выбранное слово", Dock = DockStyle.Bottom};
            deleteButton.Click += (sender, args) =>
            {
                wordsToIgnore.Remove((string)ignoredWords.SelectedItem);
                ignoredWords.Items.Remove(ignoredWords.SelectedItem);
            };

            Controls.Add(wordToExclude);
            Controls.Add(addButton);
            Controls.Add(deleteButton);
            Controls.Add(ignoredWords);
        }
    }
}
