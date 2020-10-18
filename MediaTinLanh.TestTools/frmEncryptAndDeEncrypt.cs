using MediaTinLanh.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTinLanh.TestTools
{
    public partial class frmEncryptAndDencrypt : Form
    {
        public frmEncryptAndDencrypt()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if(txtInput.Text != String.Empty)
            {
                Control_Security control_Security = new Control_Security();
                txtOutput.Text = control_Security.Encrypt(txtInput.Text);
            }    
        }

        private void btnDeEncrypt_Click(object sender, EventArgs e)
        {
            if (txtInput.Text != String.Empty)
            {
                Control_Security control_Security = new Control_Security();
                txtOutput.Text = control_Security.Decrypt(txtInput.Text);
            }
        }
    }
}
