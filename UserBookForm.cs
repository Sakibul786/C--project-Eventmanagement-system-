using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eventmanagement
{
    public partial class UserBookForm : Form
    {
        public UserBookForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            WeddingList list  = new WeddingList();
            list.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BirthdayForm list = new BirthdayForm();
            list.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            OfficeForm list = new OfficeForm();
            list.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Anniversary list = new Anniversary();
            list.Show();
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
