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

namespace MediaTinLanh.TestTools
{
    public partial class ppTOtxt : Form
    {
        public ppTOtxt()
        {
            InitializeComponent();
            
        }

        private void btnGetListFiles_Click(object sender, EventArgs e)
        {
            if(Control_Security.GET_FTP())
            {
                string[] filenames = Control_FTP.GetFileList();
                lstFiles.Items.Clear();
                foreach (string filename in filenames)
                {
                    lstFiles.Items.Add(filename);
                }
            }    
            
        }
    }
}
