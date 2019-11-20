namespace WinSandMDI_2019a.Forms
{
    partial class FormStrings
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.textBoxList1 = new System.Windows.Forms.TextBox();
            this.textBoxList2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(12, 12);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonCompare
            // 
            this.buttonCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCompare.Location = new System.Drawing.Point(93, 12);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(75, 23);
            this.buttonCompare.TabIndex = 1;
            this.buttonCompare.Text = "Compare";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.Location = new System.Drawing.Point(12, 41);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.Size = new System.Drawing.Size(431, 76);
            this.textBoxMessages.TabIndex = 3;
            this.textBoxMessages.Text = "This is for comparing strings and lists, etc. This will be the output.";
            // 
            // textBoxList1
            // 
            this.textBoxList1.Location = new System.Drawing.Point(12, 123);
            this.textBoxList1.Multiline = true;
            this.textBoxList1.Name = "textBoxList1";
            this.textBoxList1.Size = new System.Drawing.Size(378, 283);
            this.textBoxList1.TabIndex = 4;
            // 
            // textBoxList2
            // 
            this.textBoxList2.Location = new System.Drawing.Point(396, 123);
            this.textBoxList2.Multiline = true;
            this.textBoxList2.Name = "textBoxList2";
            this.textBoxList2.Size = new System.Drawing.Size(378, 283);
            this.textBoxList2.TabIndex = 5;
            // 
            // FormStrings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxList2);
            this.Controls.Add(this.textBoxList1);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.buttonCompare);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormStrings";
            this.Text = "FormStrings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.TextBox textBoxList1;
        private System.Windows.Forms.TextBox textBoxList2;
    }
}