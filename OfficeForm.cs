using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eventmanagement
{
    public partial class OfficeForm : Form
    {
        public OfficeForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(OfficeForm_Load); // Subscribe to Load event
        }

        private void OfficeForm_Load(object sender, EventArgs e)
        {
            LoadOfficeData();
        }

        private void LoadOfficeData()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM OE"; // Query to fetch data from the OE table
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Bind the DataTable to the DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Handle exceptions
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserBookForm obj = new UserBookForm();
            obj.Show();
        }
    }
}
