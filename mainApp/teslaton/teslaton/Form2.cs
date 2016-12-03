using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teslaton
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.cz/maps/place/49%C2%B012'53.2%22N+16%C2%B033'40.2%22E/@49.2147755,16.5589843,17z/data=!3m1!4b1!4m5!3m4!1s0x0:0x0!8m2!3d49.214772!4d16.561173");
        }
    }
}
