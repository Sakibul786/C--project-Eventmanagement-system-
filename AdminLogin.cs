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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
            // Set the password textbox to hide characters
            textBox2.UseSystemPasswordChar = true; // This will show '*' for password
        }

       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            {
                // Connection string to your database
                string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";

                // Create the SQL connection
                SqlConnection con = new SqlConnection(ConnectionData);

                try
                {
                    // Open the connection
                    con.Open();

                    // SQL query to check if the admin exists
                    string query = "SELECT COUNT(*) FROM Admin WHERE Id = @Id AND Password = @Password";

                    SqlCommand cmd = new SqlCommand(query, con);
                    // Admin fixed credentials
                    cmd.Parameters.AddWithValue("@Id", "2020");  // Admin Id is fixed as 2024
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text);  // Password entered by admin

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        // Login successful
                        MessageBox.Show("Admin Login Successful!");
                        this.Hide();
                        // You can redirect to the admin dashboard or another form
                        AdminDashboard adminDashboard = new AdminDashboard();
                        adminDashboard.Show();
                    }
                    else
                    {
                        // Login failed
                        MessageBox.Show("Invalid Password. Please try again.");
                        textBox2.Clear();  // Clear the password field on failure
                        textBox2.Focus();  // Focus back to the password field
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();  // Always close the connection
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Firstpage obj = new Firstpage(); // Redirect to another login form if needed
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
