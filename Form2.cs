using Ketkeorasmy_C969Project.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ketkeorasmy_C969Project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void customersButton_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();          
        }

        private void appointmentsButton_Click(object sender, EventArgs e)
        {
            Appointments appointments = new Appointments();
            appointments.Show();
        }

        private void CalendarButton_Click(object sender, EventArgs e)
        {
            Calendar calendar = new Calendar();
            calendar.Show();
        }

        private void ReportsButton_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
        }
    }
}
