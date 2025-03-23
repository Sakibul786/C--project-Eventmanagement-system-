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
    public partial class BEvent : Form
    {
        public BEvent()
        {
            InitializeComponent();
        }

        private void BEvent_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Check if all required fields are filled
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return; // Exit the event handler to prevent further processing
            }

            // Try to parse Contact_No to ensure it's a valid integer
            if (!int.TryParse(textBox3.Text, out int contactNo))
            {
                MessageBox.Show("Contact Number must be a valid number.");
                return; // Exit the event handler to prevent further processing
            }

            // Connection string to your database
            string connectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";

            // Use using statement to ensure proper disposal of SqlConnection
            using (SqlConnection con = new SqlConnection(connectionData))
            {
                try
                {
                    // Open the connection
                    con.Open();

                    // SQL command to insert data
                    SqlCommand sq2 = new SqlCommand("INSERT INTO BE(Name, Address, Contact_No, Email, Date) VALUES(@NAME, @Address, @Contact_No, @Email, @Date)", con);

                    // Add parameters
                    sq2.Parameters.AddWithValue("@NAME", textBox1.Text);
                    sq2.Parameters.AddWithValue("@Address", textBox2.Text);
                    sq2.Parameters.AddWithValue("@Contact_No", contactNo);
                    sq2.Parameters.AddWithValue("@Email", textBox4.Text);
                    sq2.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);

                    // Execute the command
                    sq2.ExecuteNonQuery();

                    // Display success message
                    MessageBox.Show("Confirmed");

                    // Write confirmation message and registration details to a text file
                    string confirmationMessage = "Booking Confirmed\n";
                    string registrationDetails = $"Name: {textBox1.Text}\n" +
                                                 $"Address: {textBox2.Text}\n" +
                                                 $"Contact No: {contactNo}\n" +
                                                 $"Email: {textBox4.Text}\n" +
                                                 $"Date: {dateTimePicker1.Value.Date}";

                    string filePath = @"E:\Project\RegistrationDetails.txt"; // Change this path if needed

                    using (StreamWriter writer = new StreamWriter(filePath, false)) // 'false' to overwrite the file
                    {
                        writer.WriteLine(confirmationMessage);
                        writer.WriteLine(registrationDetails);
                    }

                    // Open the file in Notepad
                    System.Diagnostics.Process.Start("notepad.exe", filePath);
                    // Clear textboxes
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();

                    // Switch to Event form
                    this.Hide();
                    Event obj = new Event();
                    obj.Show();
                }
                catch (Exception ex)
                {
                    // Display the error message
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Event obj = new Event();
            obj.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
