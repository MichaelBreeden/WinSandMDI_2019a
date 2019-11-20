using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WinSandMDI_2019a.Classes;

namespace WinSandMDI_2019a.Forms
{
    public partial class FormDateTime : Form
    {
        public FormDateTime()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            string str = textBoxInput1.Text;

            //Tuple<int, int, bool> tupleTimes = c_Util.TwoTimeValuesFromString(str);

            bool bIs_NowTime_BetweenTheseTwoNumbers = c_Util.bIs_NowTime_ToBePaused(str);
            //bIs_NowTime_BetweenTheseTwoNumbers = c_Util.bIs_NowTime_BetweenTheseTwoNumbers(tupleTimes.Item1, tupleTimes.Item2);

            //textBoxMessages.AppendText("Time values from string are:" + tupleTimes.Item3.ToString() + " and " + tupleTimes.Item1.ToString() + " and " + tupleTimes.Item2.ToString() + "...\r\n");
            textBoxMessages.AppendText("Should pause:" + bIs_NowTime_BetweenTheseTwoNumbers.ToString() + "...\r\n");
        }
    }
}
