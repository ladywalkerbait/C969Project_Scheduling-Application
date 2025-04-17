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
    public partial class Appointments : Form
    {
        public Appointments()
        {
            InitializeComponent();
        }

        public void Appointments_Load(object sender, EventArgs e)
        {
            SqlData();
        }
        public void SqlData()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open();

                string appointmentsQuery = @"SELECT appointment.appointmentId, appointment.type, appointment.start, appointment.end,
                                           appointment.customerId, customer.customerName, appointment.userId, user.userName
                                           FROM appointment, customer, user
                                           WHERE appointment.customerId = customer.customerId 
                                           AND appointment.userId = user.userId
                                           ORDER BY start DESC";

                MySqlCommand command = new MySqlCommand(appointmentsQuery, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);
                dgvAppointments.DataSource = data;

                dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvAppointments.MultiSelect = false;
                dgvAppointments.ReadOnly = true;

                for (int idx = 0; idx < data.Rows.Count; idx++) //checking something
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

                //for (int idx = 0; idx < data.Rows.Count; idx++)
                //{
                //    DateTime utcStart = (DateTime)data.Rows[idx]["start"];
                //    DateTime utcEnd = (DateTime)data.Rows[idx]["end"];
                //    DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(utcStart, TimeZoneInfo.Local);
                //    DateTime localEnd = TimeZoneInfo.ConvertTimeFromUtc(utcEnd, TimeZoneInfo.Local); 
                //    data.Rows[idx]["start"] = localStart;
                //    data.Rows [idx]["end"] = localEnd;
                //}
                //dgvAppointments.DataSource = null;

                //dgvAppointments.DataSource = data;
                //dgvAppointments.Refresh();
            }
        }

        private void addAppointmentButton_Click(object sender, EventArgs e)
        {
            //comparing new appointments to existing appointments
            DataTable dataTable = (DataTable)dgvAppointments.DataSource;

            addAppointment addAppointment = new addAppointment(dataTable);
            addAppointment.Show();
            this.Close();
        }

        private void UpdateAppointmentButton_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                int index = dgvAppointments.CurrentRow.Index;
                DataTable dataTable = (DataTable)dgvAppointments.DataSource;

                int customerId = (int)dataTable.Rows[index]["customerId"];
                string customerName = (string)dataTable.Rows[index]["customerName"];
                int appointmentId = (int)dataTable.Rows[index]["appointmentId"];
                string type = (string)dataTable.Rows[index]["type"];
                DateTime start = (DateTime)dataTable.Rows[index]["start"];
                DateTime end = (DateTime)dataTable.Rows[index]["end"];
                int userId = (int)dataTable.Rows[index]["userId"];
                string userName = (string)dataTable.Rows[index]["userName"]; 

                updateAppointment updateappointment = new updateAppointment(customerId, customerName, appointmentId, 
                                                                            type, start, end, userId, userName, dataTable); //added dataTable for testing 3/5
                updateappointment.ShowDialog();

                //todo Refreshing datagrid
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an appointment to modify.");
            }
        }

        private void DeleteAppointmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAppointments.CurrentRow == null || !dgvAppointments.CurrentRow.Selected)
                {
                    MessageBox.Show("Please make a selection!");
                    return;
                }
                DialogResult deleteAppointmentDialogResult = MessageBox.Show("Are you sure you want to delete this appointment?", "DeleteAppointment", MessageBoxButtons.YesNo);
                if (deleteAppointmentDialogResult == DialogResult.Yes && dgvAppointments.CurrentRow.Selected)
                {
                    int appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["appointmentId"].Value);

                    string deleteAppointment = @"DELETE FROM appointment WHERE appointmentId = @appointmentId";

                    string connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();

                    MySqlCommand deleteAppointmentCommand = new MySqlCommand(deleteAppointment, conn);
                    deleteAppointmentCommand.Parameters.AddWithValue("@appointmentId", appointmentId);
                    deleteAppointmentCommand.ExecuteNonQuery();

                    conn.Close();
                }
                SqlData(); //reload the data from the database
                dgvAppointments.Refresh(); //refresh the dgv with the data
                if (deleteAppointmentDialogResult == DialogResult.No)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            { return; }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //this is for adding the appointment to the update appointment form
        public void addAppointmentToTheDGV(int appointmentId, string type, DateTime start, DateTime end, int customerId, string customerName, int userId, string userName)
        {
            int rowIndex = dgvAppointments.Rows.Add();
            DataGridViewRow row = dgvAppointments.Rows[rowIndex];
            row.Cells["appointmentId"].Value = appointmentId;
            row.Cells["type"].Value = type;
            row.Cells["start"].Value = start;
            row.Cells["end"].Value = end;
            row.Cells["customerId"].Value = customerId;
            row.Cells["customerName"].Value = customerName;
            row.Cells["userId"].Value = userId;
            row.Cells["userName"].Value = userName;
        }
        //
    }
}
