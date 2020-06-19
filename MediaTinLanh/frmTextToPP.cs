using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
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

        private Stream pictureStream;
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string[] format = new[] {cbbFonts.Text, cbbSize.Text, cbbStyle.Text};
            Control_Presentation.CreateFiles(txtLocaltion.Text, txtContent.Text, format, pictureStream);
        }

        private void frmTextToPP_Load(object sender, EventArgs e)
        {
            for (int i = 5; i <= 100; i++)
            {
                cbbSize.Items.Add(i.ToString());
            }

            txtLocaltion.Text = Application.StartupPath;

            var ngonNgus = NgonNgusController.Context.All(); // Lấy dữ liệu từ Database qua Controller
        }

        private void btnimg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Hình ảnh (*.jpg)|*.jpg",
                Title = "Chọn một file ảnh"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureStream = File.Open(openFileDialog1.FileName, FileMode.Open);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }
    }
}
