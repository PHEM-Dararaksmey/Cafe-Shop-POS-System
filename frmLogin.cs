using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Create database 
            if (MainClass.IsValidUser(txtUsername.Text, txtPassword.Text) == false)
            {
                guna2MessageDialog1.Show("Invalid username or password");
                return;
            }
            else
            {
                this.Hide();
                frmMain Mainform = new frmMain();
                Mainform.Show();
            }

        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
