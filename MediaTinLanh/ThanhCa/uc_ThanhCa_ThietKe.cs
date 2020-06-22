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
    public partial class uc_ThanhCa_ThietKe : UserControl
    {
        private static uc_ThanhCa_ThietKe _instance;
        public static uc_ThanhCa_ThietKe Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_ThanhCa_ThietKe();
                return _instance;
            }
        }
        public uc_ThanhCa_ThietKe()
        {
            InitializeComponent();
        }

        private void uc_ThanhCa_ThietKe_Load(object sender, EventArgs e)
        {
            uc_ThanhCa_ThietKe_Line uc = new uc_ThanhCa_ThietKe_Line();
            uc.Dock = DockStyle.Fill;
            panel4.Controls.Add(uc);

        }

        private void uc_ThanhCa_ThietKe_SizeChanged(object sender, EventArgs e)
        {
            uc_ThanhCa_ThietKe_Line uc = new uc_ThanhCa_ThietKe_Line();
            uc.Dock = DockStyle.Fill;
            panel4.Controls.Add(uc);
        }
    }
}
