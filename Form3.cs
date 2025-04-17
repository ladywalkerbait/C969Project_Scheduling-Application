using Ketkeorasmy_C969Project.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Ketkeorasmy_C969Project
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            SQLData();
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Form3_Load(object sender, EventArgs e)
        {
            //SQLData();
        }
        public void SQLData()
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

            using (var conn = new MySqlConnection(loginstr))
            {
                conn.Open(); 
                string sqlQuery = @"SELECT customer.customerId, customer.customerName, address.address, address.addressId,
                                  city.city, city.cityId, country.country, country.countryId, address.phone 
                                  FROM customer, address, city, country 
                                  WHERE customer.addressID = address.addressID 
                                  AND address.cityID = city.cityID 
                                  AND city.countryID = country.countryID";

                MySqlCommand command = new MySqlCommand(sqlQuery, conn);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvCustomers.DataSource = dataTable;

                dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCustomers.MultiSelect = false;
                dgvCustomers.ReadOnly = true;
            }
        }

        private void addCustomer_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(this);
            form4.ShowDialog();
            SQLData();
            this.Close();
        }

        private void updateCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                int index = dgvCustomers.CurrentRow.Index;
                DataTable dataTable = (DataTable)dgvCustomers.DataSource;

                int customerId = (int)dataTable.Rows[index]["customerId"];
                string customerName = (string)dataTable.Rows[index]["customerName"];
                string address = (string)dataTable.Rows[index]["address"];
                string city = (string)dataTable.Rows[index]["city"];
                string country = (string)dataTable.Rows[index]["country"];
                string phone = (string)dataTable.Rows[index]["phone"];
                int addressId = (int)dataTable.Rows[index]["addressId"];
                int cityId = (int)dataTable.Rows[index]["cityId"];
                int countryId = (int)dataTable.Rows[index]["countryId"];

                Form5 form5 = new Form5(customerId, customerName, address, city, country, phone, addressId, cityId, countryId);
                form5.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("Please select a customer to modify.");
            }
            this.Close();
        }
        private void deleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCustomers.CurrentRow == null || !dgvCustomers.CurrentRow.Selected)
                {
                    MessageBox.Show("Please make a selection!");
                    return;
                }
                DialogResult deleteCustomerDialogResult = MessageBox.Show("Are you sure you want to delete this customer?", "DeleteCustomer", MessageBoxButtons.YesNo);
                if (deleteCustomerDialogResult == DialogResult.Yes && dgvCustomers.CurrentRow.Selected)
                {
                    int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["customerId"].Value);
                    int addressId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["addressId"].Value);
                    int cityId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["cityId"].Value);
                    int countryId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["countryId"].Value);

                    string deleteCustomer = @"DELETE FROM customer WHERE customerId = @customerId";
                    string deleteAddress = @"DELETE FROM address WHERE addressId = @addressId";
                    string deleteCity = @"DELETE FROM city WHERE cityId = @cityId";
                    string deleteCountry = @"DELETE FROM country WHERE countryId = @countryId";

                    string connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
                    MySqlConnection conn = new MySqlConnection(connectionString);
                    conn.Open();

                    MySqlCommand deleteCustomerCommand = new MySqlCommand(deleteCustomer, conn);
                    deleteCustomerCommand.Parameters.AddWithValue("@customerId", customerId);
                    deleteCustomerCommand.ExecuteNonQuery();

                    MySqlCommand deleteAddressCommand = new MySqlCommand(deleteAddress, conn);
                    deleteAddressCommand.Parameters.AddWithValue("@addressId", addressId);
                    deleteAddressCommand.ExecuteNonQuery();

                    MySqlCommand deleteCityCommand = new MySqlCommand(deleteCity, conn);
                    deleteCityCommand.Parameters.AddWithValue("@cityId", cityId);
                    deleteCityCommand.ExecuteNonQuery();

                    MySqlCommand deleteCountryCommand = new MySqlCommand(deleteCountry, conn);
                    deleteCountryCommand.Parameters.AddWithValue("@countryId", countryId);
                    deleteCountryCommand.ExecuteNonQuery();

                    conn.Close();

                    //dgvCustomers.Rows.RemoveAt(dgvCustomers.CurrentCell.RowIndex);
                }
                if (deleteCustomerDialogResult == DialogResult.No)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            { return; }
        }

        public void addCustomerToTheDGV(string customerName, string address, string city, string country, string phone)
        {
            int rowIndex = dgvCustomers.Rows.Add();
            DataGridViewRow row = dgvCustomers.Rows[rowIndex];
            //row.Cells["customerId"].Value = customerId;
            row.Cells["customerName"].Value = customerName;
            row.Cells["address"].Value = address;
            row.Cells["city"].Value = city;
            row.Cells["country"].Value = country;
            row.Cells["phone"].Value = phone;
        }

        public void addCustomerToTheDatabase(string customerName, string address, string city, string country, string phone)
        {

            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
              
                var transaction = conn.BeginTransaction();
                
                    try
                    {   
                    // I commented out the below to make the countryId's unique instead of the same based on country name.

                        //int countryId = Helpers.GetCountryId(conn, transaction, country);
                        //if (countryId == -1)
                        //{
                           int countryId = Helpers.InsertCountry(conn, transaction, country);
                        //}
                        int cityId = Helpers.InsertCity(conn, transaction, city, countryId);
                        int addressId = Helpers.InsertAddress(conn, transaction, address, cityId, phone);
                        Helpers.InsertCustomer(conn, transaction, customerName, addressId);
               
                        transaction.Commit();
                        MessageBox.Show("Customer was successfully added to the database.", "Successful Insert", MessageBoxButtons.OK);
                

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK);
                    }  
            }
        }

        private void cancelButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
