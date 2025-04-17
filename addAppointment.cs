using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ketkeorasmy_C969Project
{
    public partial class addAppointment : Form
    {
        private DataTable existingAppointments;
        public addAppointment(DataTable data) //checking 3/5
        {
            InitializeComponent();
            LoadCustomerNames();
            LoadAppointmentType();
            LoadAppointmentTimes();
            LoadUsers();
            existingAppointments = data;
        }

        private DataTable customerDataTable = new DataTable();
        private void LoadCustomerNames()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                string query = "SELECT customerId, customerName FROM customer";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);

                customerNameComboBox.DataSource = data;
                customerNameComboBox.DisplayMember = "customerName";
                customerNameComboBox.ValueMember = "customerId";
                
                if (data.Rows.Count > 0)
                {
                    customerNameComboBox.SelectedIndex = 0;
                }
            }
        }
        private void LoadAppointmentType()
        {
            //string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            //using (var conn = new MySqlConnection(loginstr))
            //{
            //    conn.Open();
            //    string typeQuery = "SELECT DISTINCT type FROM appointment";
            //    MySqlCommand command = new MySqlCommand(typeQuery, conn);
            //    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            //    DataTable data = new DataTable();
            //    adapter.Fill(data);

            //    typeComboBox.DataSource = data;
            //    typeComboBox.DisplayMember = "type";
            //    //comboBox1.ValueMember = "appointmentId";

            //    if (data.Rows.Count > 0)
            //    {
            //        typeComboBox.SelectedIndex = 0;
            //    }
            //}

            //I added the code below to give a list of appointment types so that if they were deleted from the database there
            //would still be choices to choose from.
            List<string> appointmentType = new List<string>
            {
                "Presentation",
                "Scrum",
                "Consultation"
            };
            typeComboBox.DataSource = appointmentType;
        }
        private void LoadAppointmentTimes()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                string timeQuery = "SELECT start, end FROM appointment"; //this doesn't show the times in the dgv, only the date
                MySqlCommand command = new MySqlCommand(timeQuery, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);

                //timeComboBox.DataSource = data;
                //timeComboBox.DisplayMember = "time";

                if (data.Rows.Count > 0)
                {
                    //timeComboBox.SelectedIndex = 0;
                }
            }
        }
        private void LoadUsers()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                string userQuery = "SELECT userId, userName FROM user";
                MySqlCommand command = new MySqlCommand(userQuery, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);

                userComboBox.DataSource = data;
                userComboBox.DisplayMember = "userName";
                userComboBox.ValueMember = "userId";

                if (data.Rows.Count > 0)
                {
                    userComboBox.SelectedIndex = 0;
                }
            }
        }

        private void customerNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customerNameComboBox.SelectedIndex == 0)
            {
                LoadCustomerNames();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeComboBox.SelectedIndex == 0)
            {
                LoadAppointmentType();
            }
        }

        private void userComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (userComboBox.SelectedIndex == 0)
            {
                LoadUsers();
            }
        }

        private void addAppointmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                //selected start and end times
                DateTime selectedStart = dateTimeStart.Value; //.Date + dateTimeStart.Value.TimeOfDay;
                DateTime selectedEnd = dateTimeEnd.Value; //.Date + dateTimeEnd.Value.TimeOfDay;

                if (!IsValidAppointmentTime(selectedStart, dateTimeEnd.Value))
                {
                    MessageBox.Show("Appointment must start between 9:00 AM and 5:00 PM, Monday to Friday.");
                    return; //prevents saving in case of overlap
                }
                if (!IsValidAppointmentTime(dateTimeStart.Value, selectedEnd))
                {
                    MessageBox.Show("Appointment must end between 9:00 AM and 5:00 PM, Monday to Friday.");
                    return; //prevents saving in case of overlap
                }

                string customerName = customerNameComboBox.Text.Trim();
                int customerId = Convert.ToInt32(customerNameComboBox.SelectedValue);
                string user = userComboBox.Text.Trim(); 
                int userId = Convert.ToInt32(userComboBox.SelectedValue);
                string type = typeComboBox.Text.Trim();

                bool hasOverlap = AppointmentOverlap(selectedStart, selectedEnd, userId);
                if (hasOverlap)
                {
                    MessageBox.Show("The selected appointment time overlaps with an existing appointment.");
                    return;
                }

                ////convert start and end times to UTC
                //TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                //DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(selectedStart, easternTimeZone);
                //DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(selectedEnd, easternTimeZone);

                TimeZoneInfo localTimeZone = TimeZoneInfo.Local; //checking if this is what is needed for A3 in the project where times added and shown are different
                DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(selectedStart, localTimeZone);
                DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(selectedEnd, localTimeZone);

                //check if time overlaps with any existing appointments
                //if (IsOverlappingWithExistingAppointments(utcStart, utcEnd))
                ////if (IsOverlap(selectedStart, selectedEnd))
                //{
                //    MessageBox.Show("The selected appointment time overlaps with an existing appointment.");
                //    return; //prevents saving in case of overlap
                //}
                //bool hasOverlap = IsOverlap(utcStart, utcEnd);
                //if (hasOverlap)
                //{
                //    MessageBox.Show("The selected appointment time overlaps with an existing appointment.");
                //    return;
                //} //prevents saving in case of overlap 

                string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
                using (var conn = new MySqlConnection(loginstr))
                {
                    conn.Open();
                    string timeQuery = $"INSERT INTO appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)" +
                                        "VALUES (@customerId, @userId, @title, @description, @location, @contact, @type, @url, @start, @end, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";

                    MySqlCommand command = new MySqlCommand(timeQuery, conn);
                    command.Parameters.AddWithValue("@customerId", customerId);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@title", "not needed");
                    command.Parameters.AddWithValue("@description", "not needed");
                    command.Parameters.AddWithValue("@location", "not needed");
                    command.Parameters.AddWithValue("@contact", "not needed");
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@url", "not needed");
                    command.Parameters.AddWithValue("@start", utcStart);
                    command.Parameters.AddWithValue("@end", utcEnd);
                    command.Parameters.AddWithValue("@createDate", DateTime.Now);
                    command.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                    command.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                    command.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                    command.ExecuteNonQuery();
                    //conn.Close();
                }
                MessageBox.Show("Appointment saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            this.Close();
            Appointments appointments = new Appointments();
            appointments.Show();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimeStart_ValueChanged(object sender, EventArgs e)
        {
            ////get selected start time
            //DateTime selectedStart = dateTimeStart.Value;

            ////validate selected start time is within hours
            //if (!IsValidAppointmentTime(selectedStart, dateTimeEnd.Value))
            //{
            //    MessageBox.Show("Appointment must start between 9:00 AM and 5:00 PM, Monday to Friday.");
            //    //dateTimeStart.Value = DateTime.Today.AddHours(9); //sets the default to 9 AM
            //}        
        }

        private void dateTimeEnd_ValueChanged(object sender, EventArgs e)
        {
            ////get selected end time
            //DateTime selectedEnd = dateTimeEnd.Value;

            ////validate selected end time is within hours
            //if (!IsValidAppointmentTime(dateTimeStart.Value, selectedEnd))
            //{
            //    MessageBox.Show("Appointment must end between 9:00 AM and 5:00 PM, Monday to Friday.");
            //    //dateTimeEnd.Value = dateTimeStart.Value.AddHours(1); //sets the default to 10 AM if start time is 9 AM
            //}

        }
        private bool IsValidAppointmentTime(DateTime start, DateTime end)
        {
            //check that times are within 9AM to 5PM Monday to Friday in EST
            TimeZoneInfo easternTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime easternStart = TimeZoneInfo.ConvertTime(start, easternTimeZone);
            DateTime easternEnd = TimeZoneInfo.ConvertTime(end, easternTimeZone);

            //check if the day is Monday to Friday, 9AM to 5PM
            return easternStart.DayOfWeek >= DayOfWeek.Monday && easternStart.DayOfWeek <= DayOfWeek.Friday
                && easternStart.Hour >= 9 && easternStart.Hour < 17
                && easternEnd.Hour >= 9 && easternEnd.Hour < 17;
        }
        private bool IsOverlappingWithExistingAppointments(DateTime start, DateTime end)
        {
            try
            {
                string query = @"SELECT COUNT(*) FROM appointment WHERE (@start < end AND @end > start)";
                string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
                using (var conn = new MySqlConnection(loginstr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    int overlapCount = Convert.ToInt32(cmd.ExecuteScalar());
                    return overlapCount > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }
        //private bool IsOverlap(DateTime selectedStart, DateTime selectedEnd)
        //{
        //    TimeZoneInfo localTimeZone = TimeZoneInfo.Local; //checking if this is what is needed for A3 in the project where times added and shown are different
        //    DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(selectedStart, localTimeZone);
        //    DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(selectedEnd, localTimeZone);

        //    DataTable dt = GetAppointmentsFromDatabase();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        DateTime existingUtcStart = (DateTime)row["start"];
        //        DateTime existingUtcEnd = (DateTime)row["end"];

        //        if (utcStart < existingUtcStart) //existingUtcEnd && utcEnd > existingUtcStart)
        //        {
        //            //return true;
        //            if (utcEnd < existingUtcStart)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            if (utcStart > existingUtcEnd)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
        private DataTable GetAppointmentsFromDatabase()
        {
            DataTable dt = new DataTable();
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                string query = @"SELECT start, end FROM appointment";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }
            return dt;
        }
        private bool AppointmentOverlap(DateTime selectedStart, DateTime selectedEnd, int userIdToUpdate) //testing for comparison 3/5
        {
            //TimeZoneInfo localTimeZone = TimeZoneInfo.Local; 
            //DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(updatedStart, localTimeZone);
            //DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(updatedEnd, localTimeZone);
            foreach (DataRow row in existingAppointments.Rows)
            {
                int existingUserId = (int)row["userId"];
                if (existingUserId != userIdToUpdate)
                {
                    continue;
                }

                DateTime existingStartLocal = (DateTime)row["start"];
                DateTime existingEndLocal = (DateTime)row["end"];
                if (selectedStart.Date == existingStartLocal.Date)
                {
                    if (selectedStart >= existingStartLocal && selectedStart <= existingEndLocal)
                    {
                        return true;
                    }
                    if (selectedEnd >= existingStartLocal && selectedEnd <= existingEndLocal)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
