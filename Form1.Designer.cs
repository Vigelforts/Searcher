namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.FilesTreeView = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.TextInFileBox = new System.Windows.Forms.TextBox();
            this.TemplateBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DirectoryBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CurrentFileName = new System.Windows.Forms.Label();
            this.numberOfParsedFiles = new System.Windows.Forms.Label();
            this.timerLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FilesTreeView
            // 
            this.FilesTreeView.Font = new System.Drawing.Font("Segoe Print", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilesTreeView.Location = new System.Drawing.Point(16, 263);
            this.FilesTreeView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.FilesTreeView.Name = "FilesTreeView";
            this.FilesTreeView.Size = new System.Drawing.Size(513, 348);
            this.FilesTreeView.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.StartButton);
            this.groupBox1.Controls.Add(this.TextInFileBox);
            this.groupBox1.Controls.Add(this.TemplateBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.DirectoryBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 17);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.groupBox1.Size = new System.Drawing.Size(513, 200);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(347, 161);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 32);
            this.button1.TabIndex = 7;
            this.button1.Text = "Остановить поиск";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(11, 161);
            this.StartButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(160, 32);
            this.StartButton.TabIndex = 6;
            this.StartButton.Text = "Начать поиск";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // TextInFileBox
            // 
            this.TextInFileBox.Location = new System.Drawing.Point(181, 101);
            this.TextInFileBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TextInFileBox.Name = "TextInFileBox";
            this.TextInFileBox.Size = new System.Drawing.Size(324, 26);
            this.TextInFileBox.TabIndex = 5;
            // 
            // TemplateBox
            // 
            this.TemplateBox.Location = new System.Drawing.Point(181, 70);
            this.TemplateBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.TemplateBox.Name = "TemplateBox";
            this.TemplateBox.Size = new System.Drawing.Size(324, 26);
            this.TemplateBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Текст в файле";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Параметры поиска";
            // 
            // DirectoryBox
            // 
            this.DirectoryBox.Location = new System.Drawing.Point(181, 29);
            this.DirectoryBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.DirectoryBox.Name = "DirectoryBox";
            this.DirectoryBox.Size = new System.Drawing.Size(324, 26);
            this.DirectoryBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Стартовая директория";
            // 
            // CurrentFileName
            // 
            this.CurrentFileName.AutoSize = true;
            this.CurrentFileName.Location = new System.Drawing.Point(13, 658);
            this.CurrentFileName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.CurrentFileName.Name = "CurrentFileName";
            this.CurrentFileName.Size = new System.Drawing.Size(0, 19);
            this.CurrentFileName.TabIndex = 2;
            // 
            // numberOfParsedFiles
            // 
            this.numberOfParsedFiles.AutoSize = true;
            this.numberOfParsedFiles.Location = new System.Drawing.Point(13, 623);
            this.numberOfParsedFiles.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.numberOfParsedFiles.Name = "numberOfParsedFiles";
            this.numberOfParsedFiles.Size = new System.Drawing.Size(0, 19);
            this.numberOfParsedFiles.TabIndex = 3;
            // 
            // timerLabel
            // 
            this.timerLabel.AutoSize = true;
            this.timerLabel.Location = new System.Drawing.Point(12, 708);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(0, 19);
            this.timerLabel.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(539, 736);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.numberOfParsedFiles);
            this.Controls.Add(this.CurrentFileName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.FilesTreeView);
            this.Font = new System.Drawing.Font("Comic Sans MS", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Form1";
            this.Text = "Поиск файлов";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView FilesTreeView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DirectoryBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextInFileBox;
        private System.Windows.Forms.TextBox TemplateBox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label CurrentFileName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label numberOfParsedFiles;
        private System.Windows.Forms.Label timerLabel;
    }
}

