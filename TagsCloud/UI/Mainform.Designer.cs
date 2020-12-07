namespace TagsCloud.UI
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.MaxWordsCountSetting = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ExcludedWordsSetButton = new System.Windows.Forms.Button();
            this.CloudSizeSetting = new System.Windows.Forms.NumericUpDown();
            this.FontFamilyChoice = new System.Windows.Forms.ComboBox();
            this.FontStyleChoice = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ImageSaveButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AlgorithmChoice = new System.Windows.Forms.ComboBox();
            this.SetImageSizeButton = new System.Windows.Forms.Button();
            this.SetPaletteButton = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWordsCountSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloudSizeSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PictureBox);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MaxWordsCountSetting);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.ExcludedWordsSetButton);
            this.splitContainer1.Panel2.Controls.Add(this.CloudSizeSetting);
            this.splitContainer1.Panel2.Controls.Add(this.FontFamilyChoice);
            this.splitContainer1.Panel2.Controls.Add(this.FontStyleChoice);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.ImageSaveButton);
            this.splitContainer1.Panel2.Controls.Add(this.StartButton);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.AlgorithmChoice);
            this.splitContainer1.Panel2.Controls.Add(this.SetImageSizeButton);
            this.splitContainer1.Panel2.Controls.Add(this.SetPaletteButton);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(800, 461);
            this.splitContainer1.SplitterDistance = 597;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // PictureBox
            // 
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(593, 457);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // MaxWordsCountSetting
            // 
            this.MaxWordsCountSetting.Location = new System.Drawing.Point(0, 339);
            this.MaxWordsCountSetting.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.MaxWordsCountSetting.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MaxWordsCountSetting.Name = "MaxWordsCountSetting";
            this.MaxWordsCountSetting.Size = new System.Drawing.Size(192, 23);
            this.MaxWordsCountSetting.TabIndex = 12;
            this.MaxWordsCountSetting.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.MaxWordsCountSetting.ValueChanged += new System.EventHandler(this.MaxWordsCountSetting_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Максимальное число слов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 388);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Размер облака";
            // 
            // ExcludedWordsSetButton
            // 
            this.ExcludedWordsSetButton.Location = new System.Drawing.Point(0, 431);
            this.ExcludedWordsSetButton.Name = "ExcludedWordsSetButton";
            this.ExcludedWordsSetButton.Size = new System.Drawing.Size(194, 23);
            this.ExcludedWordsSetButton.TabIndex = 8;
            this.ExcludedWordsSetButton.Text = "Задать исключения";
            this.ExcludedWordsSetButton.UseVisualStyleBackColor = true;
            this.ExcludedWordsSetButton.Click += new System.EventHandler(this.ExcludedWordsSetButton_Click);
            // 
            // CloudSizeSetting
            // 
            this.CloudSizeSetting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CloudSizeSetting.DecimalPlaces = 1;
            this.CloudSizeSetting.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CloudSizeSetting.Location = new System.Drawing.Point(0, 406);
            this.CloudSizeSetting.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CloudSizeSetting.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CloudSizeSetting.Name = "CloudSizeSetting";
            this.CloudSizeSetting.Size = new System.Drawing.Size(192, 19);
            this.CloudSizeSetting.TabIndex = 7;
            this.CloudSizeSetting.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            this.CloudSizeSetting.ValueChanged += new System.EventHandler(this.CloudSizeSetting_ValueChanged);
            // 
            // FontFamilyChoice
            // 
            this.FontFamilyChoice.Items.AddRange(new object[] {
            "asd",
            "dfh",
            "fgh",
            "jh"});
            this.FontFamilyChoice.Location = new System.Drawing.Point(0, 88);
            this.FontFamilyChoice.Name = "FontFamilyChoice";
            this.FontFamilyChoice.Size = new System.Drawing.Size(192, 23);
            this.FontFamilyChoice.TabIndex = 2;
            this.FontFamilyChoice.SelectedIndexChanged += new System.EventHandler(this.FontFamilyChoice_SelectedIndexChanged);
            // 
            // FontStyleChoice
            // 
            this.FontStyleChoice.Items.AddRange(new object[] {
            "asd",
            "dfh",
            "fgh",
            "jh"});
            this.FontStyleChoice.Location = new System.Drawing.Point(0, 117);
            this.FontStyleChoice.Name = "FontStyleChoice";
            this.FontStyleChoice.Size = new System.Drawing.Size(192, 23);
            this.FontStyleChoice.TabIndex = 2;
            this.FontStyleChoice.SelectedIndexChanged += new System.EventHandler(this.FontStyleChoice_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Шрифт";
            // 
            // ImageSaveButton
            // 
            this.ImageSaveButton.Location = new System.Drawing.Point(0, 254);
            this.ImageSaveButton.Name = "ImageSaveButton";
            this.ImageSaveButton.Size = new System.Drawing.Size(192, 23);
            this.ImageSaveButton.TabIndex = 6;
            this.ImageSaveButton.Text = "Сохранить в файл";
            this.ImageSaveButton.UseVisualStyleBackColor = true;
            this.ImageSaveButton.Click += new System.EventHandler(this.ImageSaveButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(0, 190);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(192, 58);
            this.StartButton.TabIndex = 4;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Алгоритм";
            // 
            // AlgorithmChoice
            // 
            this.AlgorithmChoice.Items.AddRange(new object[] {
            "asd",
            "dfh",
            "fgh",
            "jh"});
            this.AlgorithmChoice.Location = new System.Drawing.Point(0, 161);
            this.AlgorithmChoice.Name = "AlgorithmChoice";
            this.AlgorithmChoice.Size = new System.Drawing.Size(192, 23);
            this.AlgorithmChoice.TabIndex = 2;
            this.AlgorithmChoice.SelectedIndexChanged += new System.EventHandler(this.AlgorithmChoice_SelectedIndexChanged);
            // 
            // SetImageSizeButton
            // 
            this.SetImageSizeButton.Location = new System.Drawing.Point(0, 29);
            this.SetImageSizeButton.Name = "SetImageSizeButton";
            this.SetImageSizeButton.Size = new System.Drawing.Size(192, 23);
            this.SetImageSizeButton.TabIndex = 1;
            this.SetImageSizeButton.Text = "Настроить размер";
            this.SetImageSizeButton.UseVisualStyleBackColor = true;
            this.SetImageSizeButton.Click += new System.EventHandler(this.SetImageSizeButton_Click);
            // 
            // SetPaletteButton
            // 
            this.SetPaletteButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.SetPaletteButton.Location = new System.Drawing.Point(0, 0);
            this.SetPaletteButton.Name = "SetPaletteButton";
            this.SetPaletteButton.Size = new System.Drawing.Size(195, 23);
            this.SetPaletteButton.TabIndex = 0;
            this.SetPaletteButton.Text = "Настроить палитру";
            this.SetPaletteButton.UseVisualStyleBackColor = true;
            this.SetPaletteButton.Click += new System.EventHandler(this.SetPaletteButton_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "File";
            this.OpenFileDialog.Filter = "Текстовые файлы|*.txt|Документы Word|*.docx;*.doc";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "Фото|*.png";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 461);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Mainform";
            this.Text = "Mainform";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWordsCountSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloudSizeSetting)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button SetPaletteButton;
        private System.Windows.Forms.Button SetImageSizeButton;
        private System.Windows.Forms.ComboBox AlgorithmChoice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.Button ImageSaveButton;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.ComboBox FontFamilyChoice;
        private System.Windows.Forms.ComboBox FontStyleChoice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown CloudSizeSetting;
        private System.Windows.Forms.Button ExcludedWordsSetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MaxWordsCountSetting;
        private System.Windows.Forms.Label label4;
    }
}