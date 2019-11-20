using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinSandMDI_2019a.Forms
{
    public partial class FormStrings : Form
    {
        public FormStrings()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            List<string> lststrOut = new List<string>();
            bool bFound = false;
            foreach(string str1 in textBoxList1.Lines)
            {
                bFound = false;
                foreach (string str2 in textBoxList2.Lines)
                    if (str1.Trim() == str2.Trim())
                    {
                        bFound = true;
                        break;
                    }
                if(bFound == false)
                    lststrOut.Add(str1);
            }
            textBoxMessages.Lines = lststrOut.ToArray();
        }
    }
}
