namespace WinSandMDI_2019a
{
    partial class Form_Sandy
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.awaitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asyncFileCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateTimeTestingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.utilitiesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1003, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem1,
            this.awaitToolStripMenuItem,
            this.asyncFileCopyToolStripMenuItem});
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.closeToolStripMenuItem.Text = "Actions";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.actionsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem1
            // 
            this.closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            this.closeToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.closeToolStripMenuItem1.Text = "Close";
            this.closeToolStripMenuItem1.Click += new System.EventHandler(this.closeToolStripMenuItem1_Click);
            // 
            // awaitToolStripMenuItem
            // 
            this.awaitToolStripMenuItem.Name = "awaitToolStripMenuItem";
            this.awaitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.awaitToolStripMenuItem.Text = "Await";
            this.awaitToolStripMenuItem.Click += new System.EventHandler(this.awaitToolStripMenuItem_Click);
            // 
            // asyncFileCopyToolStripMenuItem
            // 
            this.asyncFileCopyToolStripMenuItem.Name = "asyncFileCopyToolStripMenuItem";
            this.asyncFileCopyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.asyncFileCopyToolStripMenuItem.Text = "Async File Copy";
            this.asyncFileCopyToolStripMenuItem.Click += new System.EventHandler(this.asyncFileCopyToolStripMenuItem_Click);
            // 
            // utilitiesToolStripMenuItem
            // 
            this.utilitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateTimeTestingToolStripMenuItem,
            this.stringsToolStripMenuItem});
            this.utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            this.utilitiesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.utilitiesToolStripMenuItem.Text = "Utilities";
            // 
            // dateTimeTestingToolStripMenuItem
            // 
            this.dateTimeTestingToolStripMenuItem.Name = "dateTimeTestingToolStripMenuItem";
            this.dateTimeTestingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.dateTimeTestingToolStripMenuItem.Text = "DateTime Testing";
            this.dateTimeTestingToolStripMenuItem.Click += new System.EventHandler(this.dateTimeTestingToolStripMenuItem_Click);
            // 
            // stringsToolStripMenuItem
            // 
            this.stringsToolStripMenuItem.Name = "stringsToolStripMenuItem";
            this.stringsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stringsToolStripMenuItem.Text = "Strings";
            this.stringsToolStripMenuItem.Click += new System.EventHandler(this.stringsToolStripMenuItem_Click);
            // 
            // Form_Sandy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 666);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_Sandy";
            this.Text = "Sandy Form ";
            this.Load += new System.EventHandler(this.Form_Sandy_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem awaitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asyncFileCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateTimeTestingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stringsToolStripMenuItem;
    }
}

