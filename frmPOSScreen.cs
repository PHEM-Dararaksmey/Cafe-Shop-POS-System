using DGVPrinterHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace POS
{
    public partial class frmPOSScreen : Form
    {
        public frmPOSScreen()
        {
            InitializeComponent();
        }
        public int MainID = 0;
        DGVPrinter printer = new DGVPrinter();
        private void frmPOSScreen_Load(object sender, EventArgs e)
        {
            dgvView.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }
        private void AddCategory()
        {
            string qry = "Select * from Category";
            SqlCommand commandd = new SqlCommand(qry, MainClass.connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            CategoryPanel.Controls.Clear();

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button button = new Guna.UI2.WinForms.Guna2Button();
                    button.FillColor = Color.FromArgb(50, 55, 89);
                    button.Size = new Size(134, 45);
                    button.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    button.Text = dataRow["catName"].ToString();
                    //event for click 
                    button.Click += new EventHandler(button_Click);
                    CategoryPanel.Controls.Add(button);
                }

            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button button = (Guna.UI2.WinForms.Guna2Button)sender;
            foreach (var item in ProductPanel.Controls)
            {
                var products = (ucProduct)item;
                products.Visible = products.PCategory.ToLower().Contains(button.Text.Trim().ToLower());
            }
        }

        private void AddItems(string id, string name , string cat, string price, Image pImage)
        {
            var ucProduct = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = pImage,           
                id = Convert.ToInt32(id)

            };

            ProductPanel.Controls.Add(ucProduct);

            ucProduct.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in dgvView.Rows)
                {
                    //Check it product alreay there then a one  to qunity and update price
                    if (Convert.ToInt32(item.Cells["dgvSerial"].Value) == wdg.id)
                 
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) * double.Parse(item.Cells["dgvPrice"].Value.ToString());
                        


                    }
                }
                //add new product
                dgvView.Rows.Add(new object[] { 0, wdg.PName, 1 ,wdg.PPrice, wdg.PPrice });
                GetTotal();
            };
        }
        //Getting product from database 
        private void LoadProducts()
        {
            //string qry = "Selcet * from products";
            string qry = "SELECT * FROM products INNER JOIN category ON catID = CategoryID ";
            SqlCommand commandd = new SqlCommand(qry, MainClass.connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            
            foreach(DataRow dataRow in dataTable.Rows)
            { 
                Byte[] imagearray = (byte[])dataRow["pImage"];
                byte[] imagebytearray = imagearray;

                AddItems(dataRow["productID"].ToString(), dataRow["pName"].ToString(), dataRow["catName"].ToString(),
                    dataRow["pPrice"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach(var item in ProductPanel.Controls)
            {
                var products = (ucProduct)item; 
                products.Visible = products.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
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
        private void GetTotal()
        {
            double total = 0;
            lbl_NumberOfPrice.Text = "";
            foreach(DataGridViewRow item in dgvView.Rows)
            {
                total += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }
            lbl_NumberOfPrice.Text = total.ToString("N2");
            
        }

        private void dgvView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvView.Rows.Clear();
            MainID = 0;
            lbl_NumberOfPrice.Text = "00";
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
         
        }

        private void btnKOT_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            // We create this functioin to helper the user get a clean printlist of stundet informatioin as pdf

            printer.Title = " M4 Cafe shop ";
            printer.SubTitle = String.Format("Date: {0} ", DateTime.Now);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Center;
            printer.Footer = "Thank you for shopping with us";
            printer.FooterSpacing = 20;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dgvView);
          


        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }
    }
}
