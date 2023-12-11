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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

       //Method to add Controls 
       private void addControls(Form form)
        {
            ControlPanel.Controls.Clear();
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            ControlPanel.Controls.Add(form);
            form.Show();
        }
        //for accessing frm main
        static frmMain _obj;
        public static frmMain Instance
        {
            get
            {
                if(_obj == null)
                {
                    _obj = new frmMain();
                }
                return _obj;
            }

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            addControls(new frmHome());
        }

        private void cbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _obj = this;
        }

        private void cbHide_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCategoris_Click(object sender, EventArgs e)
        {
            addControls(new frmCategoryView());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            addControls(new frmProduct());
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            addControls(new frmPOSScreen());

        }

        private void btnOptional_Click(object sender, EventArgs e)
        {
            
        }

        private void ControlPanel_Paint(object sender, PaintEventArgs e)
        {
            addControls(new frmHome());
        }
    }
}
