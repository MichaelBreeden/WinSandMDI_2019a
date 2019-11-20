using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinSandMDI_2019a.Forms
{
    public partial class FormAwait1 : Form
    {
        public FormAwait1()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void buttonProcess_Click(object sender, EventArgs e)
        private async void buttonProcess_Click(object sender, EventArgs e)
        {
            Task<long> task = new Task<long>(CountCharacters);
            task.Start();
            labelProcessing.Text = "Processing Data";
            long lCount = await task;
            labelProcessing.Text = "Characters added up to " + lCount.ToString() + "...";
        }

        static int iConvertCharToInt(char c)
        {
            int iValue = c - '0';
            // if (!int.TryParse(c.ToString(), out iValue)) { iValue = 0; }
            return iValue;
        }
        private long CountCharacters()
        {
            long count = 0;
            using (StreamReader reader = new StreamReader("c:\\Temp\\CorrPubLog.txt"))
            {
                string strContent = reader.ReadToEnd();
                foreach (char c in strContent.ToCharArray())
                    count += (long)iConvertCharToInt(c);
                Thread.Sleep(5000);
            }
            return count;
        }
    }
}
