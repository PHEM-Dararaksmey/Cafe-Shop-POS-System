using Guna.UI2.WinForms;
using System;
using System.Collections;
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
    public partial class frmProduct : Form
    {
        public frmProduct()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            //string qry = "SELECT * FROM products";
            
            string qry = "select productID,p.pName,p.pPrice,CategoryID, c.catName from products p inner join category c on c.catID = p.CategoryID";
            dgvView.AutoGenerateColumns = false;
            ListBox listBox = new ListBox();
            listBox.Items.Add(dgvId);
            listBox.Items.Add(dgvName);
            listBox.Items.Add(dgvPrice);
            listBox.Items.Add(dgvcatID);
            listBox.Items.Add(dgvCategory);

           
            MainClass.LoadData(qry, dgvView, listBox);

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProductAdd productAdd = new frmProductAdd();
            //productAdd.ShowDialog();
            MainClass.BlurBackground(productAdd);
            GetData();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            //For combobox file
            GetData();

        }

        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvView.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                frmProductAdd productAdd = new frmProductAdd();
                productAdd.id = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvId"].Value);
                productAdd.cID = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvcatID"].Value);
                MainClass.BlurBackground(productAdd);
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
                    string qry = "Delete from products where productID = " + id + "";
                    Hashtable hashtable = new Hashtable();
                    MainClass.SQL(qry, hashtable);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Deleted successfully");
                    GetData();
                }
            }
            /*if (dgvView.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                frmProductAdd productAdd = new frmProductAdd();
                productAdd.id = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvId"]);
                productAdd.txtName.Text = Convert.ToString(dgvView.CurrentRow.Cells["dgvName"].Value);
                productAdd.txtPrice.Text = Convert.ToString(dgvView.CurrentRow.Cells["dgvPrice"].Value);
                productAdd.cID = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvcatID"].Value);
                productAdd.ShowDialog();
                GetData();
            }
            if (dgvView.CurrentCell.OwningColumn.Name == "dgvDelete")
            {

                int id = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvId"].Value);
                string qry = "Delete from category where catID = " + id + "";
                Hashtable hashtable = new Hashtable();
                MainClass.SQL(qry, hashtable);
                MessageBox.Show("Deleted successfully");
                GetData();

            }*/
        }

    }
}
