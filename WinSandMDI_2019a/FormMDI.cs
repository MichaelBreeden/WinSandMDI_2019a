using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSandMDI_2019a.Forms;
using WinSandMDI_2019a.Classes;

namespace WinSandMDI_2019a
{
    public partial class Form_Sandy : Form
    {
        public Form_Sandy()
        {
            InitializeComponent();
        }
        private void Form_Sandy_Load(object sender, EventArgs e)
        {
            IsMdiContainer = true;
        }
        private void actionsToolStripMenuItem_Click(object sender, EventArgs e)
        { ; }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void awaitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form frm2 = new Form();
            Form formAwait1 = new FormAwait1();
            formAwait1.Show();
            formAwait1.MdiParent = this;
        }

        /// <summary>
        /// This just uses a Task to copy files in a background thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void asyncFileCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form frm2 = new Form();
            Form formFileCopyAsync1 = new Form1();
            formFileCopyAsync1.Show();
            formFileCopyAsync1.MdiParent = this;     
        }

        private void dateTimeTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formDateTime = new FormDateTime();
            formDateTime.Show();
            formDateTime.MdiParent = this;
        }

        private void stringsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formStrings = new FormStrings();
            formStrings.Show();
            formStrings.MdiParent = this;
        }
    } // End public partial class Form_Sandy : Form
}
