using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace data_base_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value <=99)
            {
                progressBar1.BackColor = Color.CadetBlue;
                progressBar1.Value += 1;
            }
            else 
            {
                
                timer1.Stop();
                home obj = new home();
                obj.Show();
                this.Hide();
               
            }

        }
    }
}
