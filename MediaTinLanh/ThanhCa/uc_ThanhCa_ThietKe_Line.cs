using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTinLanh.ThanhCa
{
    public partial class uc_ThanhCa_ThietKe_Line : UserControl
    {
        string test = "";
        public uc_ThanhCa_ThietKe_Line()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        private void uc_ThanhCa_ThietKe_Line_Load(object sender, EventArgs e)
        {
            //this.Dock = DockStyle.Fill;
            test = "";
            int li = panel1.Size.Width;
            for (int i = 0; i < li; i++)
            {
                test += ".";
            }
            label1.Text = test + "\n";
        }

        private void uc_ThanhCa_ThietKe_Line_SizeChanged(object sender, EventArgs e)
        {
            test = "";
            int li = panel1.Size.Width;
            for (int i = 0; i < li; i++)
            {
                test += ".";
            }

            label1.Text = test;
        }
    }
}
