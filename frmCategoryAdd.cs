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
    public partial class frmCategoryAdd : Form
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
        }



        public int id=0;
      

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            string qury = "";

            if (id == 0) //Insert
            {
                qury = "INSERT INTO category (catName) VALUES (@Name)";
            }
            else //update
            {
                qury = "UPDATE category SET catName = @Name WHERE catID = @id";

            }

            Hashtable hashtable = new Hashtable();
            hashtable.Add("@id", id);
            hashtable.Add("@Name", txtName.Text);

            if (MainClass.SQL(qury, hashtable) > 0)
            {
                MessageBox.Show("Saved successfully...");
                id = 0;
                txtName.Focus();
            }

        }

        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
