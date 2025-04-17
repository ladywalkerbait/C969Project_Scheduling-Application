using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ketkeorasmy_C969Project
{
    public partial class updateAppointment : Form
    {
        //testing for comparing appointments 3/5
        private DataTable existingAppointments;

        private int appointmentCustomerId;
        private int appointmentUserId;
        private string appointmentType;
        private int appointmentIdLocal;
        public updateAppointment(int customerId, string customerName, int appointmentId, string type, DateTime start,
                                 DateTime end, int userId, string userName, DataTable data) //added DataTable to compare appointment times 
        {
            InitializeComponent();

            //added to compare 3/5
            existingAppointments = data;

            //LoadCustomerNames();
            LoadAppointmentType();
            LoadUsers();

            nameComboBox1.Text = customerName;
            this.appointmentCustomerId = customerId;

            typeComboBox1.Text = type;
            this.appointmentType = type;

            startTimePicker.Text = start.ToString();
            endTimePicker.Text = end.ToString();

            userComboBox.Text = userName;
            this.appointmentUserId = userId;

            //this.appointmentId = appointmentId;
            appointmentIdLocal = appointmentId;

            LoadCustomerNames();
            //LoadAppointmentType();
            //LoadUsers();
        }
     

        private void updateButton_Click(object sender, EventArgs e)
        {
            string updatedCustomerName = nameComboBox1.Text.Trim();
            string updatedAppointmentType = typeComboBox1.Text.Trim();//SelectedItem.ToString().Trim();
            DateTime updatedStart = startTimePicker.Value; //.Date + startTimePicker.Value.TimeOfDay; //startTimePicker.Value;
            DateTime updatedEnd = endTimePicker.Value; //.Date + endTimePicker.Value.TimeOfDay; //endTimePicker.Value;
            string updatedUserName = userComboBox.Text.Trim();

            int customerId = GetCustomerIdByName(updatedCustomerName);
            int userId = GetUserIdByName(updatedUserName);

            //if (!IsValidBusinessHours(updatedStart) || !IsValidBusinessHours(updatedEnd))
            if (!IsValidAppointmentTime(updatedStart, updatedEnd))
            {
                MessageBox.Show("Appointments must be Monday-Friday between 9 AM and 5 PM EST.", "Invalid Time", MessageBoxButtons.OK);
                return;
            }
            //Comparing for overlap and ensuring none occurs for a user
            bool hasOverlap = AppointmentOverlap(updatedStart, updatedEnd, appointmentIdLocal, userId);
            if (hasOverlap)
            {
                MessageBox.Show("The selected appointment time overlaps with an existing appointment.");
                return;
            }

            //TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            //DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(updatedStart, estZone);
            //DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(updatedEnd, estZone);

            TimeZoneInfo localTimeZone = TimeZoneInfo.Local; //checking if this is what is needed for A3 in the project where times added and shown are different
            DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(updatedStart, localTimeZone);
            DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(updatedEnd, localTimeZone);

            string updateAppointmentQuery = @"UPDATE appointment 
                                              SET customerId = @customerId, type = @type, start = @start, end = @end, userId = @userId
                                              WHERE appointmentId = @appointmentId";
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(updateAppointmentQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@type", updatedAppointmentType);
                        cmd.Parameters.AddWithValue("@start", utcStart);//updatedStart);
                        cmd.Parameters.AddWithValue("@end", utcEnd);//updatedEnd);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@appointmentId", appointmentIdLocal);

                        //Console.WriteLine($"AppointmentId before executing the query: {appointmentIdLocal}");
                        //if (appointmentIdLocal <= 0)
                        //{
                        //    MessageBox.Show("Invalid appointment ID.");
                        //    return;
                        //}

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Appointment updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Appointment could not be updated.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating appointment: " + ex.Message);
                }
            }
            this.Close();
            Appointments appointments = new Appointments();
            appointments.Show();
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nameComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (nameComboBox1.SelectedIndex == 0)
            {
                LoadCustomerNames();
            }
        }

        private void typeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeComboBox1.SelectedIndex == 0)
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

        private void startTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

        private void endTimePicker_ValueChanged(object sender, EventArgs e)
        {

        }

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

                nameComboBox1.DataSource = data;
                nameComboBox1.DisplayMember = "customerName";
                nameComboBox1.ValueMember = "customerId";
                nameComboBox1.SelectedValue = appointmentCustomerId;

                for (int i = 0; i < nameComboBox1.Items.Count; i++)
                {
                    DataRowView row = nameComboBox1.Items[i] as DataRowView;
                    if (row != null && Convert.ToInt32(row["CustomerId"]) == appointmentCustomerId)
                    {
                        nameComboBox1.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        private void LoadAppointmentType()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                string typeQuery = "SELECT DISTINCT type FROM appointment";
                MySqlCommand command = new MySqlCommand(typeQuery, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);

                typeComboBox1.DataSource = data;
                typeComboBox1.DisplayMember = "type";

                //if (data.Rows.Count > 0)
                //{
                //    //typeComboBox1.SelectedIndex = 0;
                //    typeComboBox1.Text = appointmentType;
                //}
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
                //userComboBox.SelectedValue = appointmentUserId;

                //for (int i = 0; i < userComboBox.Items.Count; i++)
                //{
                //    DataRowView row = userComboBox.Items[i] as DataRowView;
                //    if (row != null && Convert.ToInt32(row["UserId"]) == appointmentUserId)
                //    {
                //        userComboBox.SelectedIndex = i;
                //        break;
                //    }
                //}
            }
        }
        private int GetCustomerIdByName(string updatedCustomerName) //Testing 2/3
        {
            int customerId = 0;
            string query = "SELECT customerId FROM customer WHERE customerName = @customerName";
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@customerName", updatedCustomerName);
                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        customerId = Convert.ToInt32(result);
                    }
                }
            }
            return customerId;
        }
        private int GetUserIdByName(string updatedUserName)
        {
            int userId = 0;
            string nameQuery = "SELECT userId FROM user WHERE userName = @userName";
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(nameQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@userName", updatedUserName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }
            return userId;
        }
        private bool IsOverlappingWithExistingAppointments(DateTime start, DateTime end)
        {
            try
            {
                //bool isOverlapping = false;
                string query = @"SELECT COUNT(*) FROM appointment WHERE (@start < end AND @end > start)";
                string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
                using (var conn = new MySqlConnection(loginstr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    //cmd.Parameters.AddWithValue("@userId", userId);
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
        public static bool HasTimeConflict(DateTime utcStart, DateTime utcEnd)
        {
            try
            {
                //TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                //DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(updatedStart, estZone);
                //DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(updatedEnd, estZone);
                //bool isOverlapping = false;
                string query = @"SELECT * FROM appointment 
                                WHERE @start < end 
                                AND @end > start";
                string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
                using (var conn = new MySqlConnection(loginstr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@start", utcStart);
                    cmd.Parameters.AddWithValue("@end", utcEnd);

                    //using (var reader = cmd.ExecuteReader())
                    //{
                    //    if (reader.HasRows)
                    //    {
                    //        while (reader.Read())
                    //        {
                    //            //DateTime existingStart = reader.GetDateTime("start");
                    //            //DateTime existingEnd = reader.GetDateTime("end");
                    //            if (reader.HasRows)
                    //            //if (utcStart < existingEnd && utcEnd > existingStart)
                    //            {
                    //                return true;
                    //            }
                    //        }
                    //    }
                    //}
                    var result = cmd.ExecuteScalar();
                    if (Convert.ToInt32(result) > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                return false;
            }
        }
        private bool AppointmentOverlap(DateTime updatedStart, DateTime updatedEnd, int appointmentIdToUpdate, int userIdToUpdate) //testing for comparison 3/5
        {
            //TimeZoneInfo localTimeZone = TimeZoneInfo.Local; 
            //DateTime utcStart = TimeZoneInfo.ConvertTimeToUtc(updatedStart, localTimeZone);
            //DateTime utcEnd = TimeZoneInfo.ConvertTimeToUtc(updatedEnd, localTimeZone);
            foreach (DataRow row in existingAppointments.Rows)
            {
                int existingAppointmentId = (int)row["appointmentId"];
                int existingUserId = (int)row["userId"];
                if (existingAppointmentId == appointmentIdToUpdate)
                {
                    continue;
                }
                if (existingUserId != userIdToUpdate)
                {
                    continue;
                }

                DateTime existingStartLocal = (DateTime)row["start"];
                DateTime existingEndLocal = (DateTime)row["end"];
                if (updatedStart.Date == existingStartLocal.Date)
                {
                    if (updatedStart >= existingStartLocal && updatedStart <= existingEndLocal)
                    {
                        return true;
                    }
                    if (updatedEnd >= existingStartLocal && updatedEnd <= existingEndLocal)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
