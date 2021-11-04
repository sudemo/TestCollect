using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoChangeDisplay
{
    public partial class ChangeDispForm1 : Form
    {
        public ChangeDispForm1()
        {
            InitializeComponent();
        }
        ChangeDisplayWrapper changeDisplayWrapper = new ChangeDisplayWrapper();
        private void button1_Click(object sender, EventArgs e)
        {

            changeDisplayWrapper.ChangeResolution(1680, 1050);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            changeDisplayWrapper.ChangeResolution(1920, 1080);

        }
    }
}
