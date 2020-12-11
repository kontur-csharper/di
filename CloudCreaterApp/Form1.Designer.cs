
namespace CloudCreaterApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.targetImagePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxFontFamily = new System.Windows.Forms.TextBox();
            this.textBoxColor = new System.Windows.Forms.TextBox();
            this.textBoxImageSize = new System.Windows.Forms.TextBox();
            this.completeFontFamily = new System.Windows.Forms.Button();
            this.completeImageSize = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxStopWord = new System.Windows.Forms.TextBox();
            this.RemoveStopWord = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxImageFormat = new System.Windows.Forms.TextBox();
            this.completeImageFormat = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.inputFilePath = new System.Windows.Forms.TextBox();
            this.addStopWord = new System.Windows.Forms.Button();
            this.completeColor = new System.Windows.Forms.Button();
            this.checkBoxRandomColor = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.log = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(17, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Исходный файл:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Путь назначения:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // targetImagePath
            // 
            this.targetImagePath.Location = new System.Drawing.Point(140, 40);
            this.targetImagePath.Name = "targetImagePath";
            this.targetImagePath.Size = new System.Drawing.Size(443, 20);
            this.targetImagePath.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(76, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Шрифт:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(88, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Цвет:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(25, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Размер изобр.:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBoxFontFamily
            // 
            this.textBoxFontFamily.Location = new System.Drawing.Point(135, 111);
            this.textBoxFontFamily.Name = "textBoxFontFamily";
            this.textBoxFontFamily.Size = new System.Drawing.Size(132, 20);
            this.textBoxFontFamily.TabIndex = 7;
            // 
            // textBoxColor
            // 
            this.textBoxColor.Location = new System.Drawing.Point(135, 141);
            this.textBoxColor.Name = "textBoxColor";
            this.textBoxColor.Size = new System.Drawing.Size(132, 20);
            this.textBoxColor.TabIndex = 8;
            // 
            // textBoxImageSize
            // 
            this.textBoxImageSize.Location = new System.Drawing.Point(135, 171);
            this.textBoxImageSize.Name = "textBoxImageSize";
            this.textBoxImageSize.Size = new System.Drawing.Size(132, 20);
            this.textBoxImageSize.TabIndex = 9;
            // 
            // completeFontFamily
            // 
            this.completeFontFamily.Location = new System.Drawing.Point(277, 111);
            this.completeFontFamily.Name = "completeFontFamily";
            this.completeFontFamily.Size = new System.Drawing.Size(76, 23);
            this.completeFontFamily.TabIndex = 10;
            this.completeFontFamily.Text = "Установить";
            this.completeFontFamily.UseVisualStyleBackColor = true;
            this.completeFontFamily.Click += new System.EventHandler(this.completeFontFamily_Click);
            // 
            // completeImageSize
            // 
            this.completeImageSize.Location = new System.Drawing.Point(277, 171);
            this.completeImageSize.Name = "completeImageSize";
            this.completeImageSize.Size = new System.Drawing.Size(76, 23);
            this.completeImageSize.TabIndex = 12;
            this.completeImageSize.Text = "Установить";
            this.completeImageSize.UseVisualStyleBackColor = true;
            this.completeImageSize.Click += new System.EventHandler(this.completeImageSize_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(373, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Стопслово:";
            // 
            // textBoxStopWord
            // 
            this.textBoxStopWord.Location = new System.Drawing.Point(455, 111);
            this.textBoxStopWord.Name = "textBoxStopWord";
            this.textBoxStopWord.Size = new System.Drawing.Size(132, 20);
            this.textBoxStopWord.TabIndex = 14;
            // 
            // RemoveStopWord
            // 
            this.RemoveStopWord.Location = new System.Drawing.Point(375, 170);
            this.RemoveStopWord.Name = "RemoveStopWord";
            this.RemoveStopWord.Size = new System.Drawing.Size(213, 23);
            this.RemoveStopWord.TabIndex = 16;
            this.RemoveStopWord.Text = "Удалить стопслово";
            this.RemoveStopWord.UseVisualStyleBackColor = true;
            this.RemoveStopWord.Click += new System.EventHandler(this.RemoveStopWord_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(21, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "Формат изобр.:";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // textBoxImageFormat
            // 
            this.textBoxImageFormat.Location = new System.Drawing.Point(135, 201);
            this.textBoxImageFormat.Name = "textBoxImageFormat";
            this.textBoxImageFormat.Size = new System.Drawing.Size(132, 20);
            this.textBoxImageFormat.TabIndex = 18;
            // 
            // completeImageFormat
            // 
            this.completeImageFormat.Location = new System.Drawing.Point(277, 201);
            this.completeImageFormat.Name = "completeImageFormat";
            this.completeImageFormat.Size = new System.Drawing.Size(76, 23);
            this.completeImageFormat.TabIndex = 19;
            this.completeImageFormat.Text = "Установить";
            this.completeImageFormat.UseVisualStyleBackColor = true;
            this.completeImageFormat.Click += new System.EventHandler(this.completeImageFormat_Click);
            // 
            // createButton
            // 
            this.createButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createButton.Location = new System.Drawing.Point(30, 250);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(533, 49);
            this.createButton.TabIndex = 22;
            this.createButton.Text = "Сгенерировать!";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(0, 100);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(600, 2);
            this.label8.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(598, -17);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(2, 400);
            this.label9.TabIndex = 24;
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(610, 10);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(330, 320);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.imageBox.TabIndex = 25;
            this.imageBox.TabStop = false;
            this.imageBox.Click += new System.EventHandler(this.imageBox_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(7, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 17);
            this.label10.TabIndex = 26;
            this.label10.Text = "Имя изображения:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(140, 70);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(443, 20);
            this.textBoxName.TabIndex = 27;
            // 
            // inputFilePath
            // 
            this.inputFilePath.Location = new System.Drawing.Point(140, 10);
            this.inputFilePath.Name = "inputFilePath";
            this.inputFilePath.Size = new System.Drawing.Size(443, 20);
            this.inputFilePath.TabIndex = 2;
            // 
            // addStopWord
            // 
            this.addStopWord.Location = new System.Drawing.Point(376, 140);
            this.addStopWord.Name = "addStopWord";
            this.addStopWord.Size = new System.Drawing.Size(213, 23);
            this.addStopWord.TabIndex = 28;
            this.addStopWord.Text = "Добавить стопслово";
            this.addStopWord.UseVisualStyleBackColor = true;
            this.addStopWord.Click += new System.EventHandler(this.addStopWord_Click);
            // 
            // completeColor
            // 
            this.completeColor.Location = new System.Drawing.Point(277, 141);
            this.completeColor.Name = "completeColor";
            this.completeColor.Size = new System.Drawing.Size(76, 23);
            this.completeColor.TabIndex = 29;
            this.completeColor.Text = "Установить";
            this.completeColor.UseVisualStyleBackColor = true;
            this.completeColor.Click += new System.EventHandler(this.completeColor_Click_1);
            // 
            // checkBoxRandomColor
            // 
            this.checkBoxRandomColor.AutoSize = true;
            this.checkBoxRandomColor.Location = new System.Drawing.Point(376, 203);
            this.checkBoxRandomColor.Name = "checkBoxRandomColor";
            this.checkBoxRandomColor.Size = new System.Drawing.Size(111, 17);
            this.checkBoxRandomColor.TabIndex = 30;
            this.checkBoxRandomColor.Text = "Рандомный цвет";
            this.checkBoxRandomColor.UseVisualStyleBackColor = true;
            this.checkBoxRandomColor.CheckedChanged += new System.EventHandler(this.checkBoxRandomColor_CheckedChanged);
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(0, 235);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(600, 2);
            this.label11.TabIndex = 31;
            // 
            // log
            // 
            this.log.AutoSize = true;
            this.log.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log.Location = new System.Drawing.Point(30, 315);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(0, 17);
            this.log.TabIndex = 32;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(365, 100);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(2, 136);
            this.label12.TabIndex = 33;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(954, 341);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.log);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.checkBoxRandomColor);
            this.Controls.Add(this.completeColor);
            this.Controls.Add(this.addStopWord);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.completeImageFormat);
            this.Controls.Add(this.textBoxImageFormat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RemoveStopWord);
            this.Controls.Add(this.textBoxStopWord);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.completeImageSize);
            this.Controls.Add(this.completeFontFamily);
            this.Controls.Add(this.textBoxImageSize);
            this.Controls.Add(this.textBoxColor);
            this.Controls.Add(this.textBoxFontFamily);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.targetImagePath);
            this.Controls.Add(this.inputFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox targetImagePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxFontFamily;
        private System.Windows.Forms.TextBox textBoxColor;
        private System.Windows.Forms.TextBox textBoxImageSize;
        private System.Windows.Forms.Button completeFontFamily;
        private System.Windows.Forms.Button completeImageSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxStopWord;
        private System.Windows.Forms.Button RemoveStopWord;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxImageFormat;
        private System.Windows.Forms.Button completeImageFormat;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox inputFilePath;
        private System.Windows.Forms.Button addStopWord;
        private System.Windows.Forms.Button completeColor;
        private System.Windows.Forms.CheckBox checkBoxRandomColor;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label log;
        private System.Windows.Forms.Label label12;
    }
}

