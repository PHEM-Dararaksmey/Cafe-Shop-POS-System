using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace POS
{
    class MainClass
    {
        // Define the connection string
        public static readonly string connectString = "Data Source = DESKTOP-SC9AEAM\\SQLEXPRESS; Initial Catalog = cafeShopDB; Integrated Security=True;";
        // Create a SqlConnection using the connection string
        public static SqlConnection connection = new SqlConnection(connectString);


        //Create method to check user vaildation
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;

            string qry = @" Select * from users where username  = '" + user + "'and upass = '" + pass + "'";
            SqlCommand command = new SqlCommand(qry, connection);
            DataTable dataTable = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            dataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                isValid = true;
                USER = dataTable.Rows[0]["uName"].ToString();
            }
            return isValid;
        }
        //Create property for username 
        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        //Create Method for CategoryAdd operation
        public static int SQL(string qry, Hashtable hashtable)
        {
            int res = 0;
            try {

                SqlCommand command = new SqlCommand(qry, connection);
                command.CommandType = CommandType.Text;

                foreach(DictionaryEntry item in hashtable)
                {
                    command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                res = command.ExecuteNonQuery();
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                connection.Close();
            }
            return res;
        }
        //For Loading data from Database

        public static int LoadData(string qry,DataGridView dataGridView, ListBox listBox )
        {
            //Serial no in gridview
            dataGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            int res = 0;
            try
            {

                SqlCommand command = new SqlCommand(qry, connection);
                command.CommandType = CommandType.Text;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;

                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    string colNumber1 = ((DataGridViewColumn)listBox.Items[i]).Name;
                    dataGridView.Columns[colNumber1].DataPropertyName = dataTable.Columns[i].ToString();
                }
         
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
                connection.Close();
            }
            return res;
        }
        //for cb Fill

        public static void CBFill(string qry,ComboBox comboBox)
        {
            SqlCommand command = new SqlCommand(qry, connection);
            command.CommandType = CommandType.Text;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            comboBox.DisplayMember = "name";
            comboBox.ValueMember = "id";
            comboBox.DataSource = dataTable;
            comboBox.SelectedIndex = -1;
        }

        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView dataGridView = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;
            
            foreach(DataGridViewRow row in dataGridView.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
        public static void BlurBackground(Form Model)
        {
            Form Backgound = new Form();
            using (Model)
            {
                Backgound.StartPosition = FormStartPosition.Manual;
                Backgound.FormBorderStyle = FormBorderStyle.None;
                Backgound.Opacity = 0.5d;
                Backgound.BackColor = Color.Black;
                Backgound.Size= frmMain.Instance.Size;
                Backgound.Location = frmMain.Instance.Location;
                Backgound.ShowInTaskbar = false;
                Backgound.Show();
                Model.Owner = Backgound;
                Model.ShowDialog(Backgound);
                Backgound.Dispose();

            }
        }
    }
}
