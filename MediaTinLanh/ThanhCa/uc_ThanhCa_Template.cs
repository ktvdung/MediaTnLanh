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
    public partial class uc_ThanhCa_Template : UserControl
    {
        public uc_ThanhCa_Template(byte[] image)
        {
            InitializeComponent();
            using (var ms = new MemoryStream(image))
            {
                pictureBox1.Image = Image.FromStream(ms);
            }
        }
    }
}
