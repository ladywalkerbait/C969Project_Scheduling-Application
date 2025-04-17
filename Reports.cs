using MySql.Data.MySqlClient;
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
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();

            //Load data
            PopulateMonthComboBox();
            LoadAppointmentTypesPerMonth();
            PopulateUserComboBox();
            LoadCustomerNames();

            //Lambda to automatically update on selection changes
            NumAppTypesComboBox.SelectedIndexChanged += (s, e) => LoadAppointmentTypesPerMonth();
            userComboBox.SelectedIndexChanged += (s, e) => LoadUserSchedule();

            //Makes the datagrids read-only 
            dgvNumAppTypes.ReadOnly = true;
            dgvNumAppTypes.AllowUserToAddRows = false;
            dgvNumAppTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvUserSchedule.ReadOnly = true;
            dgvUserSchedule.AllowUserToAddRows = false;
            dgvUserSchedule.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvNumCustomerAppointments.ReadOnly = true;
            dgvNumCustomerAppointments.AllowUserToAddRows = false;
            dgvNumCustomerAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void NumAppTypesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NumAppTypesComboBox.SelectedIndex != -1)
            {
                int selectedMonth = NumAppTypesComboBox.SelectedIndex + 1;
            }
        }

        private void dgvNumAppTypes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void PopulateMonthComboBox()
        {
            NumAppTypesComboBox.Items.Clear();
            string[] months =
                {
                    "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
                };
            NumAppTypesComboBox.Items.AddRange(months); //Adds months to the combo box

            NumAppTypesComboBox.SelectedIndex = DateTime.Now.Month - 1;
        }
        private void LoadAppointmentTypesPerMonth()
        {
            if (NumAppTypesComboBox.SelectedIndex == -1) return;
            int selectedMonth = NumAppTypesComboBox.SelectedIndex + 1;

            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                string query = @"SELECT type, COUNT(*) AS AppointmentCount
                                FROM appointment
                                WHERE MONTH(start) = @SelectedMonth
                                GROUP BY type";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SelectedMonth", selectedMonth);
                try
                {
                    conn.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    //Lambda expression to convert DataTable into a list for dgv
                    var appointmentList = datatable.AsEnumerable().Select(RowNotInTableException => new
                    {
                        Type = RowNotInTableException["type"].ToString(),
                        Count = Convert.ToInt32(RowNotInTableException["AppointmentCount"])
                    }).ToList();
                    dgvNumAppTypes.DataSource = appointmentList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvUserSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void PopulateUserComboBox()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                string userQuery = "SELECT userId, userName FROM user";
                MySqlCommand cmd = new MySqlCommand(userQuery, conn);
                try
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable data = new DataTable();
                    adapter.Fill(data);

                    userComboBox.DataSource = data;
                    userComboBox.DisplayMember = "userName";
                    userComboBox.ValueMember = "userId";
                    //userComboBox.SelectedIndex = -1;
                    if (data.Rows.Count > 0)
                    {
                        userComboBox.SelectedIndex = 0; //defaults to the first user
                        LoadUserSchedule(); //loads the first user's schedule
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading users: " + ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }
        private void LoadUserSchedule()
        {
            if (userComboBox.SelectedIndex == -1) return;
            int selectedUserId = Convert.ToInt32(userComboBox.SelectedValue);

            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                string userQuery = @"SELECT type, start, end
                                    FROM appointment
                                    WHERE userId = @userId
                                    ORDER BY start ASC";
                MySqlCommand cmd = new MySqlCommand(userQuery, conn);
                cmd.Parameters.AddWithValue("@userId", selectedUserId);
                try
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    //Lambda expression to format the data before binding it
                    var scheduleList = dt.AsEnumerable().Select(row => new
                    {
                        Type = row["type"].ToString(),
                        Start = ConvertUtcToLocal(Convert.ToDateTime(row["start"])),//.ToString("yyyy-MM-dd HH:mm"),
                        End = ConvertUtcToLocal(Convert.ToDateTime(row["end"])) //.ToString("yyyy-MM-dd HH:mm")
                    }).ToList();

                    dgvUserSchedule.DataSource = scheduleList; //Binds to dgv
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading schedule: " + ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }
        private string ConvertUtcToLocal(DateTime utcDateTime)
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local; //system's local timezone
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localZone);
            return localDateTime.ToString("yyyy-MM-dd HH:mm");
        }

        private void customerNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customerNameComboBox.SelectedIndex == -1) return;
            int selectedCustomerId = Convert.ToInt32(customerNameComboBox.SelectedValue);
            ShowCustomerAppointments(selectedCustomerId);
        }

        private void dgvNumCustomerAppointments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadCustomerNames()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                string customerQuery = "SELECT customerName, customerId FROM customer";
                MySqlCommand cmd = new MySqlCommand(customerQuery, conn);
                try
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    customerNameComboBox.DisplayMember = "customerName";
                    customerNameComboBox.ValueMember = "customerId";
                    customerNameComboBox.DataSource = table;

                    int firstCustomerId = Convert.ToInt32(customerNameComboBox.SelectedValue);
                    ShowCustomerAppointments(firstCustomerId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading customer names: " + ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }
        private void ShowCustomerAppointments(int customerId)
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                string query = @"SELECT type, COUNT(*) AS AppointmentCount
                                FROM appointment
                                WHERE customerId = @customerId
                                GROUP BY type";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                try
                {
                    conn.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable data = new DataTable();
                    adapter.Fill(data);

                    //lambda expression to filter and change the data
                    var appointmentSummary = data.AsEnumerable()
                        .Where(row => Convert.ToInt32(row["AppointmentCount"]) > 0)
                        .Select(row => new
                        {
                            Type = row["type"].ToString(),
                            AppointmentCount = Convert.ToInt32(row["AppointmentCount"])
                        })
                        .ToList();

                    dgvNumCustomerAppointments.DataSource = appointmentSummary;
                    dgvNumCustomerAppointments.Columns["AppointmentCount"].HeaderText = "Appointment Count";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading number of customer appointments: " + ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }
    }
}
