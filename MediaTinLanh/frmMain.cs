using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  MediaTinLanh.ThanhCa;

namespace MediaTinLanh
{
    public partial class frmMain : Form
    {
        private int mov;
        private int moveX;
        private int moveY;
        private int max;
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnThanhCa_Click(object sender, EventArgs e)
        {
            if (!panel4.Controls.Contains(ucThanhCa.Instance))
            {
                panel4.Controls.Add(ucThanhCa.Instance);
                ucThanhCa.Instance.Dock = DockStyle.Fill;
                ucThanhCa.Instance.BringToFront();
            }
            else
                ucThanhCa.Instance.BringToFront();
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {
            mov = 1;
            moveX = e.X;
            moveY = e.Y;
        }

        private void panel8_MouseMove(object sender, MouseEventArgs e)
        {
            if (mov==1)
            {
                this.SetDesktopLocation(MousePosition.X - moveX, MousePosition.Y - moveY);
            }
        }

        private void panel8_MouseUp(object sender, MouseEventArgs e)
        {
            mov = 0;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (max==1)
            {
                this.WindowState = FormWindowState.Normal;
                max = 0;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                max = 1;
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel8_DoubleClick(object sender, EventArgs e)
        {
            if (max == 1)
            {
                this.WindowState = FormWindowState.Normal;
                max = 0;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                max = 1;
            }
        }
    }
}
