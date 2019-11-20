namespace WinSandMDI_2019a.Forms
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
            this.textBoxInputFile = new System.Windows.Forms.TextBox();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOutputFile = new System.Windows.Forms.TextBox();
            this.buttonInputFileSelect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxInputWorkFolder = new System.Windows.Forms.TextBox();
            this.textBoxOutputWorkFolder = new System.Windows.Forms.TextBox();
            this.buttonOutputFileSelect = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCopyFile = new System.Windows.Forms.Button();
            this.buttonInputFileAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxInputFile
            // 
            this.textBoxInputFile.Location = new System.Drawing.Point(91, 58);
            this.textBoxInputFile.Name = "textBoxInputFile";
            this.textBoxInputFile.Size = new System.Drawing.Size(273, 20);
            this.textBoxInputFile.TabIndex = 0;
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Location = new System.Drawing.Point(488, 3);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.Size = new System.Drawing.Size(285, 270);
            this.textBoxMessages.TabIndex = 1;
            this.textBoxMessages.Text = "This just uses a Task.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Input File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output File:";
            // 
            // textBoxOutputFile
            // 
            this.textBoxOutputFile.Location = new System.Drawing.Point(91, 84);
            this.textBoxOutputFile.Name = "textBoxOutputFile";
            this.textBoxOutputFile.Size = new System.Drawing.Size(273, 20);
            this.textBoxOutputFile.TabIndex = 4;
            // 
            // buttonInputFileSelect
            // 
            this.buttonInputFileSelect.Location = new System.Drawing.Point(370, 56);
            this.buttonInputFileSelect.Name = "buttonInputFileSelect";
            this.buttonInputFileSelect.Size = new System.Drawing.Size(56, 23);
            this.buttonInputFileSelect.TabIndex = 5;
            this.buttonInputFileSelect.Text = "Select";
            this.buttonInputFileSelect.UseVisualStyleBackColor = true;
            this.buttonInputFileSelect.Click += new System.EventHandler(this.buttonInputFileSelect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Input Work Folder:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Output Work Folder:";
            // 
            // textBoxInputWorkFolder
            // 
            this.textBoxInputWorkFolder.Location = new System.Drawing.Point(141, 6);
            this.textBoxInputWorkFolder.Name = "textBoxInputWorkFolder";
            this.textBoxInputWorkFolder.Size = new System.Drawing.Size(341, 20);
            this.textBoxInputWorkFolder.TabIndex = 8;
            this.textBoxInputWorkFolder.Text = "\\\\colmdfs\\users$\\MBreeden:\\Temp\\_2015-11-16_MB_Test_Docs";
            // 
            // textBoxOutputWorkFolder
            // 
            this.textBoxOutputWorkFolder.Location = new System.Drawing.Point(140, 32);
            this.textBoxOutputWorkFolder.Name = "textBoxOutputWorkFolder";
            this.textBoxOutputWorkFolder.Size = new System.Drawing.Size(342, 20);
            this.textBoxOutputWorkFolder.TabIndex = 9;
            this.textBoxOutputWorkFolder.Text = "c:\\Temp\\_2015-11-16_MB_Test_Docs";
            // 
            // buttonOutputFileSelect
            // 
            this.buttonOutputFileSelect.Location = new System.Drawing.Point(370, 81);
            this.buttonOutputFileSelect.Name = "buttonOutputFileSelect";
            this.buttonOutputFileSelect.Size = new System.Drawing.Size(56, 23);
            this.buttonOutputFileSelect.TabIndex = 10;
            this.buttonOutputFileSelect.Text = "Select";
            this.buttonOutputFileSelect.UseVisualStyleBackColor = true;
            this.buttonOutputFileSelect.Click += new System.EventHandler(this.buttonOutputFileSelect_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(395, 250);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(87, 23);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonCopyFile
            // 
            this.buttonCopyFile.Location = new System.Drawing.Point(15, 130);
            this.buttonCopyFile.Name = "buttonCopyFile";
            this.buttonCopyFile.Size = new System.Drawing.Size(63, 62);
            this.buttonCopyFile.TabIndex = 12;
            this.buttonCopyFile.Text = "Copy The Dang File";
            this.buttonCopyFile.UseVisualStyleBackColor = true;
            this.buttonCopyFile.Click += new System.EventHandler(this.buttonCopyFile_Click);
            // 
            // buttonInputFileAdd
            // 
            this.buttonInputFileAdd.Location = new System.Drawing.Point(432, 56);
            this.buttonInputFileAdd.Name = "buttonInputFileAdd";
            this.buttonInputFileAdd.Size = new System.Drawing.Size(56, 23);
            this.buttonInputFileAdd.TabIndex = 13;
            this.buttonInputFileAdd.Text = "Add";
            this.buttonInputFileAdd.UseVisualStyleBackColor = true;
            this.buttonInputFileAdd.Click += new System.EventHandler(this.buttonInputFileAdd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 285);
            this.Controls.Add(this.buttonInputFileAdd);
            this.Controls.Add(this.buttonCopyFile);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOutputFileSelect);
            this.Controls.Add(this.textBoxOutputWorkFolder);
            this.Controls.Add(this.textBoxInputWorkFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonInputFileSelect);
            this.Controls.Add(this.textBoxOutputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.textBoxInputFile);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInputFile;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOutputFile;
        private System.Windows.Forms.Button buttonInputFileSelect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxInputWorkFolder;
        private System.Windows.Forms.TextBox textBoxOutputWorkFolder;
        private System.Windows.Forms.Button buttonOutputFileSelect;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCopyFile;
        private System.Windows.Forms.Button buttonInputFileAdd;
    }
}

