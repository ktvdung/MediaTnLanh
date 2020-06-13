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
    public partial class frmTextToPP : Form
    {
        public frmTextToPP()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string[] format = new[] {cbbFonts.Text, cbbSize.Text, cbbStyle.Text};
            Control_Presentation.CreateFiles(txtLocaltion.Text, txtContent.Text, format);
        }

        private void frmTextToPP_Load(object sender, EventArgs e)
        {
            for (int i = 5; i <= 100; i++)
            {
                cbbSize.Items.Add(i.ToString());
            }

            txtLocaltion.Text = Application.StartupPath;
        }
    }
}
