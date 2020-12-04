using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.Factory;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.Layouter;
using TagsCloud.TagsCloudProcessing;
using TagsCloud.TagsCloudProcessing.TagsGenerators;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.WordsConfig;

namespace TagsCloudUI
{
    public partial class ConfigWindow : Form
    {
        #region Fields
        private readonly FontDialog fontDialog;
        private readonly ColorDialog colorDialog;
        private readonly SaveFileDialog saveFileDialog;
        private readonly OpenFileDialog openFileDialog;
        private readonly TableLayoutPanel tableLayoutPanel;
        private readonly NumericUpDown widthNumeric;
        private readonly NumericUpDown heightNumeric;
        private GroupBox filterBox;
        private GroupBox convertBox;
        private GroupBox tagGeneratorsBox;
        private GroupBox layouterBox;

        private readonly WordConfig wordsConfig;
        private readonly ImageConfig imageConfig;
        private readonly TagsCloudCreator tagsCloudProcessor;
        private readonly IServiceFactory<ITextFilter> filtersFactory;
        private readonly IServiceFactory<IWordConverter> convertersFactory;
        private readonly IServiceFactory<ITagsGenerator> tagsGeneratorFactory;
        private readonly IServiceFactory<IRectanglesLayouter> layouterFactory;
        #endregion

        public ConfigWindow(WordConfig wordsConfig, ImageConfig imageConfig,
           TagsCloudCreator tagsCloudProcessor, IServiceFactory<ITextFilter> filtersFactory,
           IServiceFactory<IWordConverter> convertersFactory, IServiceFactory<ITagsGenerator> tagsGeneratorFactory,
          IServiceFactory<IRectanglesLayouter> layouterFactory)
        {
            InitializeComponent();

            this.wordsConfig = wordsConfig;
            this.imageConfig = imageConfig;
            this.tagsGeneratorFactory = tagsGeneratorFactory;
            this.layouterFactory = layouterFactory;
            this.tagsCloudProcessor = tagsCloudProcessor;
            this.filtersFactory = filtersFactory;
            this.convertersFactory = convertersFactory;

            colorDialog = new ColorDialog();
            fontDialog = new FontDialog();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Изображение (*.png)||Изображение (*.jpg)||Изображение (*.bmp)|";
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|";

            widthNumeric = new NumericUpDown();
            heightNumeric = new NumericUpDown();

            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.AutoSize = true;
            Controls.Add(tableLayoutPanel);

            InitView();
        }

        private void InitView()
        {
            var colorButton = new Button();
            AddControl(colorButton, 0, 0, "Выбрать цвет", 1, 2);
            colorButton.Click += (s, e) => colorDialog.ShowDialog();

            var fontButton = new Button();
            AddControl(fontButton, 0, 1, "Выбрать шрифт", 1, 2);
            fontButton.Click += (s, e) => fontDialog.ShowDialog();

            var saveFileButton = new Button();
            AddControl(saveFileButton, 0, 2, "Указать путь для сохранения облака", 1, 2);
            saveFileButton.Click += (s, e) => saveFileDialog.ShowDialog();

            var openButton = new Button();
            AddControl(openButton, 0, 3, "Указать путь для исходного текста", 1, 2);
            openButton.Click += (s, e) => openFileDialog.ShowDialog();

            filterBox = AddCheckBox("Выберите фильтры", filtersFactory.GetServiceNames());
            AddControl(filterBox, 0, 4, "Выберите фильтры");

            convertBox = AddCheckBox("Выберите преобразования", convertersFactory.GetServiceNames());
            AddControl(convertBox, 1, 4, "Выберите преобразования");

            tagGeneratorsBox = AddCheckBox("Выберите алгоритм постоения тега", tagsGeneratorFactory.GetServiceNames());
            AddControl(tagGeneratorsBox, 0, 5, "Выберите алгоритм постоения тега");

            layouterBox = AddCheckBox("Выберите алгоритм формирования облака", layouterFactory.GetServiceNames());
            AddControl(layouterBox, 1, 5, "Выберите алгоритм формирования облака");

            var sizeImageLabel = new Label();
            AddControl(sizeImageLabel, 0, 6, "Укажите размер изображения", 1, 2);
            sizeImageLabel.TextAlign = ContentAlignment.MiddleCenter;

            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            widthNumeric.Maximum = 5000;
            widthNumeric.Value = 500;
            AddControl(widthNumeric, 0, 7, "");

            heightNumeric.Maximum = 5000;
            heightNumeric.Value = 500;
            AddControl(heightNumeric, 1, 7, "");

            var accept = new Button();
            AddControl(accept, 0, 8, "Принять настройки", 1, 2);
            accept.Click += AcceptSettings;

            var showButton = new Button();
            showButton.Dock = DockStyle.Bottom;
            showButton.Text = "Показать изображение";
            showButton.Click += ShowImage;
            Controls.Add(showButton);
        }

        private void AddControl(Control control, int column, int row, string text, int spanRow = 1, int spanColumn = 1)
        {
            control.Text = text;
            control.Dock = DockStyle.Fill;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            tableLayoutPanel.Controls.Add(control, column, row);
            tableLayoutPanel.SetColumnSpan(control, spanColumn);
            tableLayoutPanel.SetRowSpan(control, spanRow);
        }

        private GroupBox AddCheckBox(string boxName, IEnumerable<string> names)
        {
            var box = new GroupBox();
            box.Text = boxName;
            foreach (var name in names)
            {
                var checkBox = new CheckBox();
                checkBox.Text = name;
                checkBox.Dock = DockStyle.Top;
                box.Controls.Add(checkBox);
            }
            return box;
        }

        private void AcceptSettings(object sender, EventArgs eventArgs)
        {
            var imageSize = new Size((int)widthNumeric.Value, (int)heightNumeric.Value);

            wordsConfig.Color = colorDialog.Color;
            wordsConfig.Font = fontDialog.Font;
            wordsConfig.Path = openFileDialog.FileName;
            wordsConfig.FilersNames = BindGroupBoxControls(filterBox);
            wordsConfig.ConvertersNames = BindGroupBoxControls(convertBox);
            wordsConfig.LayouterName = BindGroupBoxControls(layouterBox).First();
            wordsConfig.TagGeneratorName = BindGroupBoxControls(tagGeneratorsBox).First();

            imageConfig.ImageSize = imageSize;
            imageConfig.Path = saveFileDialog.FileName;
        }

        private string[] BindGroupBoxControls(GroupBox box)
        {
            var names = new List<string>();
            foreach (var control in box.Controls)
                if ((control is CheckBox checkBox) && checkBox.Checked)
                    names.Add(checkBox.Text);
            return names.ToArray();
        }

        private void ShowImage(object sender, EventArgs eventArgs)
        {
            tableLayoutPanel.Hide();
            Size = imageConfig.ImageSize;
            tagsCloudProcessor.CreateCloud(wordsConfig.Path, imageConfig.Path);
            using var image = Image.FromFile(imageConfig.Path);
            using var graphics = CreateGraphics();
            graphics.DrawImage(image, new PointF(0, 0));
        }
    }
}
