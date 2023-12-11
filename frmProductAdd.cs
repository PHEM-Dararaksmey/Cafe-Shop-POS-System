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
using System.Windows.Forms;

namespace POS
{
    public partial class frmProductAdd : Form
    {
        public frmProductAdd()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        public int id = 0;
        public int cID= 0;

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";

            if (id == 0) //Insert
            {
                qry = "INSERT INTO products  VALUES (@Name, @Price , @cat , @img)";
            }
            else //update
            {
                qry = "UPDATE products SET pName = @Name , pPrice = @Price , CategoryID = @cat ,pImage = @img WHERE productID = @id";

            }
            //Image 
            Image image = new Bitmap(txtImage.Image);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream,System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = memoryStream.ToArray();

            Hashtable hashtable = new Hashtable();  
            hashtable.Add("@id", id);
            hashtable.Add("@Name", txtName.Text);
            hashtable.Add("@Price", txtPrice.Text);
            //hashtable.Add("@Amount", txtAmount.Text);
            hashtable.Add("@img",imageByteArray);
            hashtable.Add("@cat", Convert.ToInt32(cbCat.SelectedValue));

            if (MainClass.SQL(qry, hashtable) > 0)
            {
                MessageBox.Show("Saved successfully...");
                id = 0;
                txtName.Text = "";
                txtPrice.Text = "";
                cbCat.SelectedIndex = -1; 
                
               
            }

        }
        private void forUpdateLoadData()
        {
            string qry = @"Select * from products where  = " + id + "";
            SqlCommand commandd = new SqlCommand(qry, MainClass.connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            
            if (dataTable.Rows.Count > 0)
            {
                txtName.Text = dataTable.Rows[0]["pName"].ToString();
                txtPrice.Text = dataTable.Rows[0]["pPrice"].ToString();

                Byte[] imageArray = (byte[])(dataTable.Rows[0]["pImage"]);
                Byte[] imageByteArray = imageArray;
                txtImage.Image = Image.FromStream(new MemoryStream(imageArray));

            }

        }
        Byte[] imageByteArray;
        string filePath;
      
        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Image Files(*.jpg, *.png, *.jpeg, *.gif, *.bmp) | *.jpg; *.png; *.jpeg; *.gif; *.bmp";
            openFile.Title = "Select an image";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filePath = openFile.FileName;
                txtImage.Image = new Bitmap(filePath);
            }
        }

        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            //for combolbox fill

            string qry = "select catID 'id', catName 'name' from category ";

            MainClass.CBFill(qry,cbCat);

            if(cID > 0) //for update
            {
                cbCat.SelectedValue = cID;
            }
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }

}

