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
    public partial class uc_ThanhCa_ThuVienBaiHat : UserControl
    {
        private static uc_ThanhCa_ThuVienBaiHat _instance;
        public static uc_ThanhCa_ThuVienBaiHat Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_ThanhCa_ThuVienBaiHat();
                return _instance;
            }
        }
        public uc_ThanhCa_ThuVienBaiHat()
        {
            InitializeComponent();
        }
    }
}
