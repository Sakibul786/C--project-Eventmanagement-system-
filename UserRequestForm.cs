using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Eventmanagement
{
    public partial class UserRequestForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";
        private string userId;

        public UserRequestForm(string userId)
        {
            InitializeComponent();
            this.userId = userId; // Store the logged-in user's ID
        }

        private void UserRequestForm_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate user input
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                // Insert request into the UserRequest table
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO UserRequest (UserId, Name, Address, Contact_No, Email, RequestDate, Status, Date) " +
                                   "VALUES (@UserId, @Name, @Address, @ContactNo, @Email, GETDATE(), 'Pending', @Date)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Address", textBox2.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Email", textBox4.Text);
                        cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date); // Get the selected date

                        // Open the connection and execute the query
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Request submitted successfully.");
                    }
                }
                // Use the specific file path to save the registration details
                string filePath = @"E:\Project\RegistrationDetails.txt";

                // Prepare the registration details string
                string registrationDetails = $"Your registration details:\n" +
                                             $"User ID: {userId}\n" +
                                             $"Name: {textBox1.Text}\n" +
                                             $"Address: {textBox2.Text}\n" +
                                             $"Contact Number: {textBox3.Text}\n" +
                                             $"Email: {textBox4.Text}\n" +
                                             $"Request Date: {DateTime.Now}\n" +
                                             $"Event Date: {dateTimePicker1.Value.Date}\n\n";

                // Append the registration details to the file
                System.IO.File.AppendAllText(filePath, registrationDetails);


                // Clear fields after successful submission
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                dateTimePicker1.Value = DateTime.Today; // Reset DateTimePicker to today
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserRequestForm_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //UserNotificationsForm notificationsForm = new UserNotificationsForm(loggedInUserId);
            //notificationsForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            Fs obj = new Fs(userId);
            obj.Show();
        }
    }
}


