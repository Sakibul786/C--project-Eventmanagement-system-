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
    public partial class Event : Form
    {
        private string userId;
        public Event()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AEvent obj = new AEvent();
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mevent obj = new Mevent();
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BEvent obj = new BEvent();
            obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Oevent obj = new Oevent();
            obj.Show();
        }

        private void Event_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            this.Hide();
            Fs obj = new Fs(userId);
            obj.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }
    }
}
