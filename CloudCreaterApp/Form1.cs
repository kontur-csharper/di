using Autofac;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TagsCloudContainer;

namespace CloudCreaterApp
{
    public partial class Form1 : Form
    {
        public TagsCloudCreator creator;
        public Form1()
        {
            InitializeComponent();
            var scope = Configurator.GetContainer().BeginLifetimeScope();
            creator = scope.Resolve<TagsCloudCreator>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var project_path = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(
                Path.GetDirectoryName(Directory.GetCurrentDirectory()))), "TagsCloudContainer");
            inputFilePath.Text = Path.Combine(project_path, "input.txt");
            targetImagePath.Text = project_path;
            textBoxName.Text = "TagCloud";
            textBoxFontFamily.Text = "Arial";
            textBoxColor.Text = "Black";
            textBoxImageSize.Text = "300";
            textBoxImageFormat.Text = "png";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void completeFontFamily_Click(object sender, EventArgs e)
        {
            if (creator.TrySetFontFamily(textBoxFontFamily.Text))
                log.Text = "Установлен шрифт: " + textBoxFontFamily.Text;
            else
                MessageBox.Show("Такой шрифт не поддерживается", "Ошибка");
        }

        private void imageBox_Click(object sender, EventArgs e)
        {

        }

        private void createButton_Click(object sender, EventArgs e)
        {
            var fullImagePath = Path.Combine(targetImagePath.Text, textBoxName.Text + "." + creator.GetImageFormat());
            if (!Directory.Exists(targetImagePath.Text))
                MessageBox.Show("Неверный путь назначения", "Ошибка");
            else if (!File.Exists(inputFilePath.Text))
                MessageBox.Show("Неверный путь исходного файла", "Ошибка");
            else if (textBoxName.Text == "")
                MessageBox.Show("Введите имя файла", "Ошибка");
            else if (File.Exists(fullImagePath))
                if (!AskForRewriting(fullImagePath))
                    return;
            creator.Create(inputFilePath.Text, targetImagePath.Text, textBoxName.Text);
            imageBox.Image = Image.FromFile(fullImagePath);
            log.Text = "Облако успешно сгенерировано";
        }

        private bool AskForRewriting(string fullImagePath)
        {
            var result = MessageBox.Show("Изображение с таким именем уже существует, желаете его перезаписать?",
                "Предупреждение", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (imageBox.Image != null)
                    imageBox.Image.Dispose();
                File.Delete(fullImagePath);
                return true;
            }

            return false;
        }

        private void RemoveStopWord_Click(object sender, EventArgs e)
        {
            if (textBoxStopWord.Text != "")
            {
                creator.RemoveStopWord(textBoxStopWord.Text);
                log.Text = "Стопслово " + textBoxStopWord.Text + " удалено";
            }
        }

        private void addStopWord_Click(object sender, EventArgs e)
        {
            if (textBoxStopWord.Text != "")
            {
                creator.AddStopWord(textBoxStopWord.Text);
                log.Text = "Стопслово " + textBoxStopWord.Text + " добавлено";
            }
        }

        private void completeColor_Click_1(object sender, EventArgs e)
        {
            if (textBoxColor.Text != "")
                if (!creator.TrySetFontColor(textBoxColor.Text))
                    MessageBox.Show("Такой цвет не поддерживается. \nИспользуйте английские наименования цветов", "Ошибка");
                else
                    log.Text = "Установлен цвет: " + textBoxColor.Text;
        }

        private void completeImageSize_Click(object sender, EventArgs e)
        {
            if (textBoxImageSize.Text != "")
            {
                int size;
                if (int.TryParse(textBoxImageSize.Text, out size) && creator.TrySetImageSize(size))
                {
                    log.Text = "Установлен размер: " + textBoxImageSize.Text + " на " + textBoxImageSize.Text;
                }
                else
                    MessageBox.Show("Неверный размер изображения \n(не больше 2000px и не меньше 100px)", "Ошибка");
            }
        }

        private void completeImageFormat_Click(object sender, EventArgs e)
        {
            if (textBoxImageFormat.Text != "")
            {
                if (!creator.TrySetImageFormat(textBoxImageFormat.Text))
                    MessageBox.Show("Неверный формат изображения", "Ошибка");
                else
                    log.Text = "Установлен формат: " + textBoxImageFormat.Text;
            }
        }

        private void checkBoxRandomColor_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox) sender).Checked)
            {
                textBoxColor.Enabled = false;
                completeColor.Enabled = false;
                creator.SetFontRandomColor();
                log.Text = "Установлен рандомный цвет";
            }
            else
            {
                textBoxColor.Enabled = true;
                completeColor.Enabled = true;
                creator.TrySetFontColor("Black");
                log.Text = "Установлен цвет: Black";
            }
        }
    }
}
