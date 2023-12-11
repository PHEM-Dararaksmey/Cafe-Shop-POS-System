using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class frmCategoryView : Form
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string qry = "SELECT *  FROM category";
            //string qry = "SELECT * FROM Category WHERE catName LIKE ' % " + txtSearch.Text + " % '";
            ListBox listBox = new ListBox();
            listBox.Items.Add(dgvId);
            listBox.Items.Add(dgvName);
            // set the visible property  
            //dgvView.Columns["catID"].Visible = false;
            //dgvView.Columns["catName"].Visible = false;

            dgvView.AutoGenerateColumns = false;
            MainClass.LoadData(qry, dgvView, listBox);

        }
        public virtual void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void frmCategoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new frmCategoryAdd());
            //frmCategoryAdd CategoryAdd = new frmCategoryAdd();
            //CategoryAdd.ShowDialog();

            GetData();
        }



        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        
            if (dgvView.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                frmCategoryAdd categoryAdd = new frmCategoryAdd();
                categoryAdd.id = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvId"].Value);
                categoryAdd.txtName.Text = Convert.ToString(dgvView.CurrentRow.Cells["dgvName"].Value);
                categoryAdd.ShowDialog();
                MainClass.BlurBackground(categoryAdd);
                GetData();
            }
            if (dgvView.CurrentCell.OwningColumn.Name == "dgvDelete")
            {
                // need to confirm before delete
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvId"].Value);
                    string qry = "Delete from category where catID = " + id + "";
                    Hashtable hashtable = new Hashtable();
                    MainClass.SQL(qry, hashtable);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Deleted successfully");
                    GetData();
                }
            }


        }
    }

}
