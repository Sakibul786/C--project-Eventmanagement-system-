using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Eventmanagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // Mask the password with '*' characters
            textBox2.PasswordChar = '*';
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection con = new SqlConnection(ConnectionData))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Id FROM EF WHERE Id = @Id AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text);

                    string userId = cmd.ExecuteScalar()?.ToString();

                    if (!string.IsNullOrEmpty(userId))
                    {
                        // Login successful, open UserDashboard
                        Fs userDashboard = new Fs(userId);
                        userDashboard.Show();
                        this.Hide(); // Hide login form
                    }
                    else
                    {
                        MessageBox.Show("Invalid Id or Password. Please try again.");
                        textBox2.Clear();
                        textBox2.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register obj = new Register();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Firstpage L = new Firstpage();
            L.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
