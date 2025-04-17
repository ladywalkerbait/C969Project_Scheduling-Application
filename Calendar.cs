using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ketkeorasmy_C969Project
{
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();
            LoadCalendar();
            PopulateMonthComboBox();
        }

        private void dgvCalendar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AllAppointmentsButton_Click(object sender, EventArgs e)
        {
            LoadCalendar();
            MonthComboBox.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void monthDayPicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = monthDayPicker.Value;
            LoadAppointmentsForDay(selectedDate);
            //if (selectedDate.Year != DateTime.Now.Year)
            //{
            //    MessageBox.Show("You can only view appointments within the current year.", "Invalid Selection");
            //    monthDayPicker.Value = DateTime.Today;
            //    return;
            //}
            //if (selectedDate.Day == 1)
            //{
            //    LoadAppointmentsForDay(selectedDate);
            //}

        }
        private void LoadAppointmentsForDay(DateTime date)
        {
            try
            {
                string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

                using (var conn = new MySqlConnection(loginstr))
                {
                    conn.Open();
                    string dayQuery = @"SELECT appointment.start, appointment.end, appointment.type,
                                             customer.customerName, user.userName
                                             FROM appointment
                                             INNER JOIN customer
                                             ON appointment.customerId = customer.customerId
                                             INNER JOIN user ON appointment.userId = user.userId
                                             WHERE DATE(appointment.start) = @Date
                                             ORDER BY appointment.start";
                    MySqlCommand cmd = new MySqlCommand(dayQuery, conn);
                    cmd.Parameters.AddWithValue("@Date", date.Date);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count == 0)
                    {
                        MessageBox.Show("There are no appointments scheduled on this day.", "No Appointments", MessageBoxButtons.OK);
                    }
                    else
                    {
                        dgvCalendar.DataSource = table;
                        for (int idx = 0; idx < table.Rows.Count; idx++) //added this to ensure data shows in local time
                        {
                            table.Rows[idx]["start"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)table.Rows[idx]["start"], TimeZoneInfo.Local);
                            table.Rows[idx]["end"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)table.Rows[idx]["end"], TimeZoneInfo.Local);
                        } //
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error loading appointments for the selected day: {ex.Message}");
            }
        }

        //public void Calendar_Load(object sender, EventArgs e)
        //{
        //    LoadCalendar();
        //    PopulateMonthComboBox();
        //    MonthComboBox.Items.Add("January");


        //}
        public void LoadCalendar()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();

                string appointmentsQuery = @"SELECT DISTINCT appointment.start, appointment.end, appointment.type,
                                           customer.customerName, user.userName
                                           FROM appointment, customer, user
                                           WHERE appointment.customerId = customer.customerId 
                                           AND appointment.userId = user.userId
                                           ORDER BY Start";

                MySqlCommand command = new MySqlCommand(appointmentsQuery, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);

                dgvCalendar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCalendar.MultiSelect = false;
                dgvCalendar.ReadOnly = true;

                dgvCalendar.DataSource = data;
                for (int idx = 0; idx < data.Rows.Count; idx++) //added this to ensure data shows in local time
                {
                    data.Rows[idx]["start"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)data.Rows[idx]["start"], TimeZoneInfo.Local);
                    data.Rows[idx]["end"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)data.Rows[idx]["end"], TimeZoneInfo.Local);
                } //

                //foreach (DataRow row in data.Rows)
                //{
                //    DateTime utcStart = Convert.ToDateTime(row["start"]);
                //    DateTime utcEnd = Convert.ToDateTime(row["end"]);

                //    //Convert to local time
                //    DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(utcStart, TimeZoneInfo.Local);
                //    DateTime localEnd = TimeZoneInfo.ConvertTimeFromUtc(utcEnd, TimeZoneInfo.Local);

                //    //Update with converted local times
                //    row["start"] = localStart;
                //    row["end"] = localEnd;
                //}

                //dgvCalendar.DataSource = data;
            }
        }
        private void LoadAppointmentsForMonth(int month)
        {
            try
            {
                string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
                using (var conn = new MySqlConnection(loginstr))
                {
                    conn.Open();
                    string monthQuery = @"SELECT appointment.start, appointment.end, appointment.type,
                                           customer.customerName, user.userName
                                           FROM appointment
                                           INNER JOIN customer ON appointment.customerId = customer.customerId
                                           INNER JOIN user ON appointment.userId = user.userId
                                           WHERE MONTH(appointment.start) = @Month AND YEAR(appointment.start) = @Year 
                                           ORDER BY appointment.start";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(monthQuery, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("Month", month);
                    adapter.SelectCommand.Parameters.AddWithValue("Year", DateTime.Now.Year);

                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    if (data.Rows.Count == 0)
                    {
                        MessageBox.Show("There are no appointments scheduled this month.", "No Appointments", MessageBoxButtons.OK);
                    }
                    else
                    {
                        dgvCalendar.DataSource = data;
                        for (int idx = 0; idx < data.Rows.Count; idx++) //added this to ensure data shows in local time
                        {
                            data.Rows[idx]["start"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)data.Rows[idx]["start"], TimeZoneInfo.Local);
                            data.Rows[idx]["end"] = TimeZoneInfo.ConvertTimeFromUtc((DateTime)data.Rows[idx]["end"], TimeZoneInfo.Local);
                        } //
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}");
            }
        }
        private void PopulateMonthComboBox()
        {
            MonthComboBox.Items.Clear();
            string[] months =
                {
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
                };
            MonthComboBox.Items.AddRange(months); //Adds months to the combo box
            
            //MonthComboBox.Items.Clear();
            //for (int month = 1; month <= 12; month++)
            //{
            //    MonthComboBox.Items.Add(new DateTime(DateTime.Now.Year, month, 1).ToString("MMMM"));
            //}
            MonthComboBox.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void MonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MonthComboBox.SelectedIndex != -1)
            {
                int selectedMonth = MonthComboBox.SelectedIndex + 1;
                //int selectedYear = DateTime.Now.Year;
                LoadAppointmentsForMonth(selectedMonth);
            }
        }
    }
}
