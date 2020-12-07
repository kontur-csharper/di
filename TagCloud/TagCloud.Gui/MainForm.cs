using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TagCloud.Core.Utils;
using TagCloud.Gui.Helpers;
using TagCloud.Gui.InputModels;
using TagCloud.Gui.Utils;

namespace TagCloud.Gui
{
    public partial class MainForm : Form, IUi
    {
        private FormWindowState previousState;
        private Image? currentResultImage;

        public MainForm()
        {
            InitializeComponent();
            previousState = WindowState;
        }

        public event Action? ExecutionRequested;

        public void Run()
        {
            Application.Run(this);
        }

        public void OnAfterWordDrawn(Image newImage, Color backgroundColor)
        {
            pictureBox.BackColor = backgroundColor;
            if (newImage != currentResultImage)
            {
                currentResultImage?.Dispose();
                currentResultImage = newImage;
                UpdatePreviewImage();
            }
        }

        public UiLockingOperation StartLockingOperation()
        {
            var ctSource = new CancellationTokenSource();

            ExecuteButton.Enabled = false;
            StopButton.Enabled = true;
            StopButton.Click += StopButtonClickHandler;
            StopButton.Focus();
            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.MarqueeAnimationSpeed = 20;

            return new UiLockingOperation(ctSource.Token, progressBar: progressBar,
                disposer: new ActionDisposable(() =>
                {
                    ExecuteButton.Enabled = true;
                    StopButton.Enabled = false;
                    StopButton.Click -= StopButtonClickHandler;
                    ExecuteButton.Focus();
                    progressBar.Style = ProgressBarStyle.Blocks;
                }));

            void StopButtonClickHandler(object _, EventArgs __) => ctSource.Cancel();
        }

        public void AddUserInput<T>(UserInputOneOptionChoice<T> inputModel)
        {
            CreateUserInputContainer(inputModel.Description, () =>
            {
                var combobox = new ComboBox();

                combobox.Items.AddRange(inputModel.Available.Select(x => x.Name).Cast<object>().ToArray());
                combobox.SelectedItem = inputModel.Selected.Name;

                combobox.SelectionChangeCommitted +=
                    (_, __) => inputModel.SetSelected(combobox.SelectedItem.ToString()!);

                return combobox;
            });
        }

        public void AddUserInput<T>(UserInputMultipleOptionsChoice<T> inputModel)
        {
            CreateUserInputContainer(inputModel.Description, () =>
            {
                var treeView = new TreeView {CheckBoxes = true, AutoSize = true, Scrollable = true};

                treeView.Nodes.AddRange(
                    inputModel.Available
                        .Select(x => new TreeNode(x.Name) {Checked = inputModel.IsSelected(x.Name)})
                        .ToArray()
                );

                treeView.AfterCheck += (_, args) => inputModel.SetSelection(args.Node.Text, args.Node.Checked);
                return treeView;
            });
        }

        public void AddUserInput(UserInputField fieldInput)
        {
            CreateUserInputContainer(fieldInput.Description, () =>
            {
                var textBox = new TextBox();
                fieldInput.LinkTo(textBox);
                return textBox;
            });
        }

        public void AddUserInput(UserInputColor colorInput)
        {
            CreateUserInputContainer(null, () =>
            {
                var colorButton = new Button
                {
                    Text = colorInput.Description,
                    BackColor = colorInput.Picked,
                    ForeColor = Color.Black,
                    Size = new Size(30, 30),
                    FlatStyle = FlatStyle.Flat
                };

                var colorDialog = CreateColorDialog();
                colorButton.Click += (_, __) =>
                {
                    colorDialog.ShowDialog(this);
                    colorInput.Picked = colorDialog.Color;
                    colorButton.BackColor = colorDialog.Color;
                };

                return colorButton;
            });
        }

        public void AddUserInput(UserInputColorPalette colorInput)
        {
            CreateUserInputContainer(colorInput.Description, () =>
            {
                var table = new TableLayoutPanel
                {
                    ColumnCount = 2,
                    ColumnStyles = {new ColumnStyle(SizeType.Percent, 50), new ColumnStyle(SizeType.Percent, 50)},
                    RowCount = 2,
                    RowStyles = {new RowStyle(SizeType.Percent, 100), new RowStyle(SizeType.Absolute, 30)}
                };

                var colorsList = new ListView {Dock = DockStyle.Fill};
                colorsList.Items.AddRange(colorInput.PickedColors.Select(c => new ListViewItem
                    {
                        Name = c.Name,
                        Text = c.Name,
                        ForeColor = c
                    })
                    .ToArray()
                );
                table.Controls.Add(colorsList, 0, 0);
                table.SetColumnSpan(colorsList, 2);

                var addColorButton = new Button {Text = "Add", Dock = DockStyle.Fill};
                addColorButton.Click += (_, __) =>
                {
                    using var colorDialog = CreateColorDialog();
                    colorDialog.ShowDialog();
                    var pickedColor = colorDialog.Color;
                    if (colorInput.PickedColors.Contains(pickedColor))
                        return;

                    colorInput.AddColor(pickedColor);
                    colorsList.Items.Add(new ListViewItem
                    {
                        Name = pickedColor.Name,
                        Text = pickedColor.Name,
                        ForeColor = pickedColor
                    });
                };
                table.Controls.Add(addColorButton, 0, 1);

                var removeColorButton = new Button {Text = "Remove", Dock = DockStyle.Fill};
                removeColorButton.Click += (_, __) =>
                {
                    if (colorInput.PickedColors.Count == 0)
                        return;

                    var selected = colorsList.SelectedItems.Count == 0
                        ? new[] {colorInput.PickedColors.Last()}
                        : colorsList.SelectedItems.Cast<ListViewItem>().Select(x => x.ForeColor).ToArray();

                    foreach (var color in selected)
                    {
                        colorInput.RemoveColor(color);
                        colorsList.Items.RemoveByKey(color.Name);
                    }
                };
                table.Controls.Add(removeColorButton, 1, 1);

                return table;
            });
        }

        public void AddUserInput(UserInputSizeField sizeInput)
        {
            CreateUserInputContainer(sizeInput.Description, () =>
            {
                var table = new TableLayoutPanel
                {
                    BorderStyle = BorderStyle.None,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                    Padding = Padding.Empty,
                    Margin = Padding.Empty,
                    RowCount = 1,
                    RowStyles = {new RowStyle(SizeType.Percent, 100)},
                    ColumnCount = 4,
                    ColumnStyles =
                    {
                        new ColumnStyle(SizeType.Percent, 25),
                        new ColumnStyle(SizeType.Percent, 25),
                        new ColumnStyle(SizeType.Percent, 25),
                        new ColumnStyle(SizeType.Percent, 25)
                    },
                    Height = 30
                };

                var isPoint = sizeInput.ShowAsPoint;

                table.Controls.Add(
                    new Label {Dock = DockStyle.Fill, AutoSize = true, Text = isPoint ? "X coord" : "Width"}, 0, 0);
                var widthInput = CreateIntInput(sizeInput.Width, i => sizeInput.Width = i, DockStyle.Fill);
                table.Controls.Add(widthInput, 1, 0);

                table.Controls.Add(
                    new Label {Dock = DockStyle.Fill, AutoSize = true, Text = isPoint ? "Y coord" : "Height"}, 2, 0);
                var heightInput = CreateIntInput(sizeInput.Height, i => sizeInput.Height = i, DockStyle.Fill);
                table.Controls.Add(heightInput, 3, 0);
                return table;
            });
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            UpdatePreviewImage();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (WindowState != previousState)
            {
                previousState = WindowState;
                if (WindowState != FormWindowState.Minimized)
                    UpdatePreviewImage();
            }
        }

        private void ExecuteButton_Click(object sender, EventArgs args)
        {
            ExecutionRequested?.Invoke();
        }

        private void CreateUserInputContainer(string? showingText, Func<Control> innerCreator)
        {
            var offset = new Size(10, 5);
            var startY = rightPanel.Controls.Cast<Control>().Select(x => x.Bottom).MaxOrDefault();

            var panel = new Panel
            {
                Location = new Point(0, startY + offset.Height),
                Width = rightPanel.Width - offset.Width * 2
            };
            rightPanel.Controls.Add(panel);

            var itemWidth = (panel.Size - offset * 2).Width;
            var nextY = offset.Height;

            if (showingText != null)
            {
                var label = CreateDescriptionLabel(showingText, new Point(offset.Width, nextY), itemWidth);
                panel.Controls.Add(label);
                nextY = label.Bottom + offset.Height / 3;
            }

            var inner = innerCreator.Invoke();
            inner.Location = new Point(offset.Width, nextY);
            inner.Width = itemWidth;
            panel.Controls.Add(inner);

            panel.Height = inner.Bottom;
        }

        private static Label CreateDescriptionLabel(string text, Point location, int width) => new Label
        {
            Text = text,
            Location = location,
            Width = width
        };

        private static NumericUpDown CreateIntInput(int initialValue, Action<int> onValueChanged, DockStyle dockStyle)
        {
            var numUpDown = new NumericUpDown
            {
                Dock = dockStyle,
                Value = initialValue,
                Minimum = int.MinValue,
                Maximum = int.MaxValue
            };

            numUpDown.KeyPress += (_, args) =>
                args.Handled = !(char.IsDigit(args.KeyChar) || args.KeyChar == '\b' || args.KeyChar == '-');
            numUpDown.ValueChanged += (_, __) =>
                onValueChanged.Invoke((int) numUpDown.Value);

            return numUpDown;
        }

        private void UpdatePreviewImage()
        {
            if (currentResultImage == null)
                return;

            var previous = pictureBox.Image;
            pictureBox.Image = GraphicsUtils.FitToSize(currentResultImage, pictureBox.Size);
            previous?.Dispose();
        }

        private ColorDialog CreateColorDialog()
        {
            return new ColorDialog
            {
                AnyColor = false,
                ShowHelp = false,
                FullOpen = true,
                AllowFullOpen = true
            };
        }
    }
}