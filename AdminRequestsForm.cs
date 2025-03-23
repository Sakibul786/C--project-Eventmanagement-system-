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
    public partial class AdminRequestsForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";

        public AdminRequestsForm()
        {
            InitializeComponent();
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM UserRequest WHERE Status = 'Pending'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dataTable = new DataTable();

                try
                {
                    con.Open();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Bind to DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading requests: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             UpdateRequestStatus("Accepted");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdminRequestsForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateRequestStatus("Rejected");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadRequests();
        }
        private void UpdateRequestStatus(string status)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int requestId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RequestId"].Value);
                int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserId"].Value);

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Update the request status
                        string updateQuery = "UPDATE UserRequest SET Status = @Status WHERE RequestId = @RequestId";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
                        {
                            updateCmd.Parameters.AddWithValue("@Status", status);
                            updateCmd.Parameters.AddWithValue("@RequestId", requestId);
                            updateCmd.ExecuteNonQuery();
                        }

                        // Insert notification for the user
                        if (status == "Accepted")
                        {
                            string notificationQuery = "INSERT INTO Notifications (UserId, Message, NotificationDate) VALUES (@UserId, @Message, GETDATE())";
                            using (SqlCommand notificationCmd = new SqlCommand(notificationQuery, con))
                            {
                                string message = $"Your request (ID: {requestId}) has been accepted.";
                                notificationCmd.Parameters.AddWithValue("@UserId", userId);
                                notificationCmd.Parameters.AddWithValue("@Message", message);
                                notificationCmd.ExecuteNonQuery();
                            }

                            // Step 3: Open the Event form after accepting the request
                            OpenEventForm(); // Call the method to open the Event form
                        }

                        MessageBox.Show($"Request {status.ToLower()} successfully.");
                        LoadRequests(); // Refresh the DataGridView
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a request to update.");
            }
        }

        private void OpenEventForm()
        {
            // Create an instance of the Event form
            Event eventForm = new Event(); // Create a new instance of your Event form
            eventForm.Show(); // Show the Event form
            this.Hide(); // Optionally hide the AdminRequestsForm if you want to navigate away from it
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ConfirmRequest();
        }

        private void ConfirmRequest()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int requestId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RequestId"].Value);

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string updateQuery = "UPDATE UserRequest SET Status = 'Confirmed' WHERE RequestId = @RequestId";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@RequestId", requestId);

                        try
                        {
                            con.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Request confirmed successfully.");
                                LoadRequests(); // Refresh the request list
                            }
                            else
                            {
                                MessageBox.Show("Request not found or already confirmed.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error confirming request: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a request to confirm.");
            }
        }
         private void button2_Click_1(object sender, EventArgs e)
         {
            UpdateRequestStatus("Accepted");
         }

        private void button3_Click_1(object sender, EventArgs e)
        {
            UpdateRequestStatus("Rejected");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadRequests();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard obj = new AdminDashboard();
            obj.Show();
        }
    }
}




