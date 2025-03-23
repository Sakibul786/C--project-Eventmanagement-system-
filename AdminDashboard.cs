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
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            
        }

        // Event handler for the button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of the UserInfoForm
            UserInfoForm userInfoForm = new UserInfoForm();

            // Show the UserInfoForm
            userInfoForm.Show();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            UserBookForm obj = new UserBookForm();
            obj.Show();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminRequestsForm obj = new AdminRequestsForm();
            obj.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
