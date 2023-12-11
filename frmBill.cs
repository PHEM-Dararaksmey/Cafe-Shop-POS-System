using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace POS
{
    public partial class frmBill : Form
    {
        public frmBill()
        {
            InitializeComponent();
        }

        private void guna2ControlBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int MainID = 0;
        private void loadData()
        {

           

         /*   //string qry = "Selcet * from products";
            string qry = @"SELECT SaleID , ProductName, Quanity,Price,Total FROM where status <> 'Pending'  "; 
            ListBox listBox = new ListBox();
            listBox.Items.Add(dgvSerial);
            listBox.Items.Add(dgvName);
            listBox.Items.Add(dgvPrice);
            listBox.Items.Add(dgvQty);
            listBox.Items.Add(dgvAmount);


            MainClass.LoadData(qry, dgvView, listBox);
         */
        } 

        private void frmBill_Load(object sender, EventArgs e)
        {
            //loadData();
        }

       

        private void dgvView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //For seari NO
            int count = 0;

            foreach (DataGridViewRow row in dgvView.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
        private void dgvView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvView.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                
                MainID = Convert.ToInt32(dgvView.CurrentRow.Cells["dgvId"].Value);
                this.Close();
                
            }
            


        }
    }
}
