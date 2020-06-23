using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
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
            for (int i = 0; i < 15; i++)
            {
                uc_ThanhCa_ThietKe_Line uc=  new uc_ThanhCa_ThietKe_Line();
                uc.Dock = DockStyle.Top;
                uc.BringToFront();
                flowLayoutPanel1.Controls.Add(uc);
            }
            foreach (string file in Directory.EnumerateFiles(Application.StartupPath+ @"\tmp\pp\jpg\BaiHat2006175162856\", "*.jpg"))
            {
                Image img= Image.FromFile(file);
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, img.RawFormat);
                    uc_ThanhCa_ThietKe_Slide ucThanhCaThietKeSlide= new uc_ThanhCa_ThietKe_Slide(ms.ToArray());
                    ucThanhCaThietKeSlide.Dock = DockStyle.Top;
                    ucThanhCaThietKeSlide.BringToFront();
                    flowLayoutPanel2.Controls.Add(ucThanhCaThietKeSlide);
                }
            }
            //Image img2 = Image.FromFile(Application.StartupPath + @"\template\pp\thumb_template1.jpg");
            //using (var ms2 = new MemoryStream())
            //{
            //    img2.Save(ms2, img2.RawFormat);
            //    uc_ThanhCa_Template ucThanhCaThietKeTemplate = new uc_ThanhCa_Template(ms2.ToArray());
            //    ucThanhCaThietKeTemplate.Dock = DockStyle.Left;
            //    ucThanhCaThietKeTemplate.BringToFront();
            //    flowLayoutPanel3.Controls.Add(ucThanhCaThietKeTemplate);
            //}
            foreach (string file2 in Directory.EnumerateFiles(Application.StartupPath + @"\template\pp\", "*.jpg"))
            {
                Image img2 = Image.FromFile(file2);
                using (var ms2 = new MemoryStream())
                {
                    img2.Save(ms2, img2.RawFormat);
                    uc_ThanhCa_Template ucThanhCaThietKeTemplate = new uc_ThanhCa_Template(ms2.ToArray());
                    flowLayoutPanel3.Controls.Add(ucThanhCaThietKeTemplate);
                }
            }
        }

        private void uc_ThanhCa_ThietKe_SizeChanged(object sender, EventArgs e)
        {
            
        }
    }
}
