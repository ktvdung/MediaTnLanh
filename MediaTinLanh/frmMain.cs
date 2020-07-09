using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTinLanh.Control;

namespace MediaTinLanh
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            
        }

        private int max = 0;
        private int min = 0;
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (max == 0)
            {
                this.WindowState = FormWindowState.Maximized;
                max = 1;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                max = 0;
            }
                
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var ngonNgus = NgonNgusController.Context.All(); // Lấy dữ liệu từ Database qua Controller
        }
    }
}
