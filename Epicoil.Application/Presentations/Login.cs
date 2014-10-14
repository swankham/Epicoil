using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Epicoil.Appl
{
    public partial class Login : BaseSession
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            epiSession = null;
            Application.Exit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strUser = txtUsername.Text.ToString();
            string strPass = txtPassword.Text.ToString();

            var uccUserInfo = this.SessionIdentify(strUser, strPass);

            if (uccUserInfo != null)
            {
                this.Hide();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(sender, e);
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                txtPassword.SelectAll();
            }
        }
    }
}