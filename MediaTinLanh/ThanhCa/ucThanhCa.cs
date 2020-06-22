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
    public partial class ucThanhCa : UserControl
    {
        private static ucThanhCa _instance;
        public static ucThanhCa Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ucThanhCa();
                return _instance;
            }
        }
        public ucThanhCa()
        {
            InitializeComponent();
        }

        private void btnThuVienBaiHat_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(uc_ThanhCa_ThuVienBaiHat.Instance))
            {
                panel3.Controls.Add(uc_ThanhCa_ThuVienBaiHat.Instance);
                uc_ThanhCa_ThuVienBaiHat.Instance.Dock = DockStyle.Fill;
                uc_ThanhCa_ThuVienBaiHat.Instance.BringToFront();
            }
            else
                uc_ThanhCa_ThuVienBaiHat.Instance.BringToFront();
            btnThuVienBaiHat.BackColor = Color.FromArgb(52, 52, 52);
        }

        private void ucThanhCa_Load(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(uc_ThanhCa_ThuVienBaiHat.Instance))
            {
                panel3.Controls.Add(uc_ThanhCa_ThuVienBaiHat.Instance);
                uc_ThanhCa_ThuVienBaiHat.Instance.Dock = DockStyle.Fill;
                uc_ThanhCa_ThuVienBaiHat.Instance.BringToFront();
            }
            else
                uc_ThanhCa_ThuVienBaiHat.Instance.BringToFront();
            btnThuVienBaiHat.BackColor = Color.FromArgb(52, 52, 52);
            btnTuTaoTrinhChieu.BackColor= Color.FromArgb(83, 83, 83);
        }


        private void btnTuTaoTrinhChieu_Click(object sender, EventArgs e)
        {
            if (!panel3.Controls.Contains(uc_ThanhCa_ThietKe.Instance))
            {
                panel3.Controls.Add(uc_ThanhCa_ThietKe.Instance);
                uc_ThanhCa_ThietKe.Instance.Dock = DockStyle.Fill;
                uc_ThanhCa_ThietKe.Instance.BringToFront();
            }
            else
                uc_ThanhCa_ThietKe.Instance.BringToFront();
            btnThuVienBaiHat.BackColor = Color.FromArgb(83, 83, 83);
            btnTuTaoTrinhChieu.BackColor = Color.FromArgb(52, 52, 52);
        }
    }
}
