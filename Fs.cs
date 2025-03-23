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
    public partial class Fs : Form
    {
        private string userId;

        public Fs(string id) // Constructor receives user ID
        {
            InitializeComponent();
            userId = id; // Store user ID

            // Initially hide all text fields and labels
            HideUserDataFields();
            button5.Visible = false; // Hide the update button
            button9.Visible = false;

        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {
            // Optionally, load some default info or instructions here
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Load user data and show text fields when the button is clicked
            LoadUserData();
        }

        private void LoadUserData()
        {
            string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(ConnectionData))
            {
                try
                {
                    con.Open();
                    string query = "SELECT Name, Email, Contact_No, Password FROM EF WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate textboxes with user data
                        textBox1.Text = reader["Name"].ToString();
                        textBox2.Text = reader["Email"].ToString();
                        textBox3.Text = reader["Contact_No"].ToString();
                        textBox4.Text = reader["Password"].ToString(); // Mask if needed

                        // Show text fields and update button now that data is available
                        ShowUserDataFields();
                        button5.Visible = true; // Show the update button
                        button9.Visible=true;
                    }
                    else
                    {
                        MessageBox.Show("User data not found.");
                        HideUserDataFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void HideUserDataFields()
        {
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;

            // Hide labels or any other controls related to user data
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
        }

        // Method to show text fields
        private void ShowUserDataFields()
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;

            // Show labels or any other controls related to user data
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Event obj = new Event();
            obj.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login();
            obj.Show();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Call the method to update user info
            UpdateUserData();
        }

        private void UpdateUserData()
        {
            string connectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection con = new SqlConnection(connectionData))
            {
                try
                {
                    con.Open();

                    // Check if any data has changed
                    string query = "SELECT Name, Email, Contact_No, Password FROM EF WHERE Id = @Id";
                    SqlCommand checkCmd = new SqlCommand(query, con);
                    checkCmd.Parameters.AddWithValue("@Id", userId);

                    SqlDataReader reader = checkCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Get the existing data
                        string existingName = reader["Name"].ToString();
                        string existingEmail = reader["Email"].ToString();
                        string existingContactNo = reader["Contact_No"].ToString();
                        string existingPassword = reader["Password"].ToString();

                        // Check if the new data is different from the existing data
                        if (existingName == textBox1.Text &&
                            existingEmail == textBox2.Text &&
                            existingContactNo == textBox3.Text &&
                            existingPassword == textBox4.Text)
                        {
                            // No data has changed, show no message
                            MessageBox.Show("No changes detected.");
                            HideUserDataFields();
                            button5.Visible = false; // Hide the update button
                            button9.Visible = false;
                            return; // Return early to avoid executing the update query
                        }
                    }
                    reader.Close();

                    // If changes are detected, proceed to update
                    string updateQuery = "UPDATE EF SET Name = @Name, Email = @Email, Contact_No = @Contact_No, Password = @Password WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(updateQuery, con);

                    // Get updated values from textboxes
                    cmd.Parameters.AddWithValue("@Id", userId); // Use the user ID to find the right record
                    cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Email", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Contact_No", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox4.Text); // Ensure to hash this in a real app

                    // Execute the command
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("User information updated successfully!");
                        // Hide text fields and update button after successful update
                        HideUserDataFields();
                        button5.Visible = false; // Hide the update button
                        button9.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("No user found with that ID.");
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserRequestForm userRequestForm = new UserRequestForm(userId);
            userRequestForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserNotificationsForm obj = new UserNotificationsForm(userId);
            obj.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Fs_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            HideUserDataFields();
            button5.Visible = false; // Hide the update button
            button9.Visible = false;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Confirm logout action
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Clear any user session data if applicable
                // For example, if you have a userId or similar, you can set it to null or clear it

                // Hide the current form
                this.Hide();

                // Show the login form
                Login loginForm = new Login();
                loginForm.Show();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login obj = new Login();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Aboutus  obj = new Aboutus();
            obj.Show();

        }
    }
}
