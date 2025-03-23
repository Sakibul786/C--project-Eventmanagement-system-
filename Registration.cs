using System;
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

namespace Eventmanagement
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Check if all required fields are filled
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return; // Exit the event handler to prevent further processing
            }

            // Try to parse Contact_No to ensure it's a valid integer
            int contactNo;
            if (!int.TryParse(textBox4.Text, out contactNo))
            {
                MessageBox.Show("Contact Number must be a valid number.");
                return; // Exit the event handler to prevent further processing
            }

            // Connection string to your database
            string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";

            // Create the SQL connection
            SqlConnection con = new SqlConnection(ConnectionData);

            try
            {
                // Open the connection
                con.Open();

                // SQL command to insert data
                SqlCommand sq2 = new SqlCommand("INSERT INTO EF(Id, Name, Email, Contact_No, Password) VALUES(@Id, @NAME, @Email, @Contact_No, @Password)", con);

                // Add parameters
                sq2.Parameters.AddWithValue("@Id", textBox1.Text);
                sq2.Parameters.AddWithValue("@NAME", textBox2.Text);
                sq2.Parameters.AddWithValue("@Email", textBox3.Text);
                sq2.Parameters.AddWithValue("@Contact_No", contactNo); // Use parsed integer value
                sq2.Parameters.AddWithValue("@Password", textBox5.Text);

                // Execute the command
                sq2.ExecuteNonQuery();

                // Display success message
                MessageBox.Show("Successfully registered!");
                // Write registration details to a text file
                string filePath = @"E:\Project\RegistrationDetails.txt"; // Change this path if needed
                string registrationDetails = "Your registration details:\n" +
                                             $"ID: {textBox1.Text}\n" +
                                             $"Name: {textBox2.Text}\n" +
                                             $"Email: {textBox3.Text}\n" +
                                             $"Contact No: {contactNo}\n" +
                                             $"Password: {textBox5.Text}"; // For security, avoid storing passwords in plain text like this.

                using (StreamWriter writer = new StreamWriter(filePath, false)) // 'false' to overwrite the file
                {
                    writer.WriteLine(registrationDetails);
                }

                // Open the file in Notepad
                System.Diagnostics.Process.Start("notepad.exe", filePath);

                // Clear textboxes
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

                // Switch to Form1
                this.Hide();
                Login obj = new Login();
                obj.Show();
            }
            catch (Exception ex)
            {
                // Display the error message
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                // Always close the connection
                con.Close();
            }
        }

    


    private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
                this.Hide();
                Login form1 = new Login();
                form1.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
