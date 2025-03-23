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
    public partial class UserNotificationsForm : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Project\EV.mdf;Integrated Security=True;Connect Timeout=30";
        private string userId;

        public UserNotificationsForm(string userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadNotifications();
        }

        private void LoadNotifications()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT Message, NotificationDate FROM Notifications WHERE UserId = @UserId AND IsRead = 0"; // Fetch unread notifications
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Assuming you have a DataGridView named dgvNotifications
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading notifications: " + ex.Message);
                }
            }
        }
        private void UserNotificationsForm_Load(object sender, EventArgs e)
        {
            // Optionally, you can call LoadNotifications() again here if needed
            // LoadNotifications();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MarkNotificationsAsRead(); // This should match the method name below
        }

        // Updated method name
        private void MarkNotificationsAsRead()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE Notifications SET IsRead = 1 WHERE UserId = @UserId AND IsRead = 0"; // Mark all notifications as read
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@UserId", userId);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("All notifications marked as read.");
                    LoadNotifications(); // Refresh the notifications
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error marking notifications as read: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
