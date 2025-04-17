using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Ketkeorasmy_C969Project
{
    public partial class Form5 : Form
    {
        private int customerIdLocal;
        private int addressIdLocal;
        private int cityIdLocal;
        private int countryIdLocal;

        public Form5(int customerId, string customerName, string address, string city, string country, string phone, 
                     int addressId, int cityId, int countryId)
        {
            InitializeComponent();
            
            nameTextBox1.Text = customerName;
            addressTextBox1.Text = address;
            cityTextBox1.Text = city;
            countryTextBox1.Text = country;
            phoneTextBox1.Text = phone;

            customerIdLocal = customerId;
            addressIdLocal = addressId;
            cityIdLocal = cityId;
            countryIdLocal = countryId;
        }

        private void nameTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void addressTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cityTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void countryTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void phoneTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void updateCustomerButton_Click(object sender, EventArgs e)
        {
            //fields are trimmed
            string updatedCustomerName = nameTextBox1.Text.Trim();
            string updatedAddress = addressTextBox1.Text.Trim();
            string updatedCity = cityTextBox1.Text.Trim();
            string updatedCountry = countryTextBox1.Text.Trim();
            string updatedPhone = phoneTextBox1.Text.Trim();

            //fields are non-empty
            if (string.IsNullOrEmpty(updatedCustomerName))
            {
                MessageBox.Show("Customer name is required.");
                nameTextBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(updatedAddress))
            {
                MessageBox.Show("Address is required.");
                addressTextBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(updatedCity))
            {
                MessageBox.Show("City is required.");
                cityTextBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(updatedCountry))
            {
                MessageBox.Show("Country is required.");
                countryTextBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(updatedPhone))
            {
                MessageBox.Show("Phone is required.");
                phoneTextBox1.Focus();
                return;
            }

            string updateCustomer = @"UPDATE customer SET customerName = @customerName WHERE customerId = @customerId";
            string updateAddress = @"UPDATE address SET address = @address WHERE addressId = @addressId"; 
            string updateCity = @"UPDATE city SET city = @city WHERE cityId = @cityId";
            string updateCountry = @"UPDATE country SET country = @country WHERE countryId = @countryId";
            string updatePhone = @"UPDATE address SET phone = @phone WHERE addressId = @addressId"; 

            string connectionString = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand customerCommand = new MySqlCommand(updateCustomer, conn);
            customerCommand.Parameters.AddWithValue("@customerName", updatedCustomerName);
            customerCommand.Parameters.AddWithValue("@customerId", customerIdLocal);
            customerCommand.ExecuteNonQuery();

            //Add code for updating address table
            MySqlCommand addressCommand = new MySqlCommand(updateAddress, conn);
            addressCommand.Parameters.AddWithValue("@address", updatedAddress);
            addressCommand.Parameters.AddWithValue("@addressId", addressIdLocal);
            addressCommand.ExecuteNonQuery();

            //Add code for updating city table
            MySqlCommand cityCommand = new MySqlCommand(updateCity, conn);
            cityCommand.Parameters.AddWithValue("@city", updatedCity);
            cityCommand.Parameters.AddWithValue("@cityId", cityIdLocal);
            cityCommand.ExecuteNonQuery();

            //Add code for updating country table
            MySqlCommand countryCommand = new MySqlCommand(updateCountry, conn);
            countryCommand.Parameters.AddWithValue("@country", updatedCountry);
            countryCommand.Parameters.AddWithValue("@countryId", countryIdLocal);
            countryCommand.ExecuteNonQuery();

            //Code for updating phone number
            MySqlCommand phoneCommand = new MySqlCommand(updatePhone, conn);
            phoneCommand.Parameters.AddWithValue("@phone", updatedPhone);
            phoneCommand.Parameters.AddWithValue("@addressId", addressIdLocal);
            phoneCommand.ExecuteNonQuery();

            MessageBox.Show("Customer was successfully updated in the database.", "Successful Update", MessageBoxButtons.OK);
            //conn.Close();
            Form3 form3 = new Form3();
            form3.ShowDialog();
            this.Close();
            
        }

        public void updateCustomerInTheDatabase(string customerName, string address, string city, string country, string phone)
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
                    int newCountryId = Helpers.GetCountryId(conn, transaction, country);
                    int newCityId = Helpers.GetCityId(conn, transaction, city, newCountryId);
                    int addressId = Helpers.GetAddressId(conn, transaction, address);
                    Helpers.UpdateAddress(conn, transaction, addressId, address, newCityId, phone);
                    Helpers.UpdateCustomerInfo(conn, transaction, customerName, addressId);
                    //int countryId = Helpers.GetCountryId(conn, transaction, country);
                    //if (countryId == -1)
                    //{
                    //    countryId = Helpers.InsertCountry(conn, transaction, country);
                    //    //countryId = Helpers.GetCountryId(conn, transaction, country); //testing
                    //}
                    //else
                    //{
                    //    string updateCountryQuery = @"UPDATE country SET country = @country, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE countryId = @countryId";
                    //    using (var cmd = new MySqlCommand(updateCountryQuery, conn, transaction))
                    //    {
                    //        cmd.Parameters.AddWithValue("@country", country);
                    //        //cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                    //        //cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                    //        cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                    //        cmd.Parameters.AddWithValue("@lasUpdateBy", Helpers.CurrentUser);
                    //        cmd.Parameters.AddWithValue("@countryId", countryId); //added this in to check
                    //        cmd.ExecuteNonQuery();
                    //    }
                    //}
                    //        int cityId = Helpers.GetCityId(conn, transaction, city, countryId);
                    //        if (cityId == -1)
                    //        {
                    //            cityId = Helpers.InsertCity(conn, transaction, city, countryId);
                    //            //cityId = Helpers.GetCityId(conn, transaction, city, countryId);
                    //        }
                    //        else
                    //        {
                    //            string updateCityQuery = @"UPDATE city SET city = @city, countryId = @countryId, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE cityId = @cityId";
                    //            using (var cmd = new MySqlCommand(updateCityQuery, conn, transaction))
                    //            {
                    //                cmd.Parameters.AddWithValue("@city", city);
                    //                cmd.Parameters.AddWithValue("@countryId", countryId);
                    //                //cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                    //                //cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                    //                cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                    //                cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                    //                cmd.ExecuteNonQuery();
                    //            }
                    //        }
                    //        int addressId = Helpers.GetAddressId(conn, transaction, address);
                    //        //if (addressId == -1)
                    //        //{
                    //        //    addressId = Helpers.InsertAddress(conn, transaction, address, cityId, phone);
                    //        //    //addressId = Helpers.GetAddressId(conn, transaction, address);
                    //        //}
                    //        //else
                    //        //{
                    //            string updateAddressQuery = @"UPDATE address SET address = @address, cityId = @cityId, phone = @phone, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE addressId = @addressId";
                    //            using (var cmd = new MySqlCommand(updateAddressQuery, conn, transaction))
                    //            {
                    //                cmd.Parameters.AddWithValue("@address", address);
                    //                cmd.Parameters.AddWithValue("@cityId", cityId);
                    //                cmd.Parameters.AddWithValue("@phone", phone);
                    //                cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                    //                cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                    //                cmd.Parameters.AddWithValue("@addressId", addressId);
                    //                cmd.ExecuteNonQuery();
                    //            }
                    //       // }
                    //        int customerId = Helpers.GetCustomerId(conn, transaction, customerName);
                    //        //if (customerId == -1)
                    //        //{
                    //        //    //customerId = Helpers.GetCustomerId(conn, transaction, address);
                    //        //    //customerId = Helpers.InsertCustomer(conn, transaction, customerName, addressId);
                    //        //}
                    //        //else
                    //        //{
                    //        string updateCustomerQuery = @"UPDATE customer SET customerName = @customerName, addressId = @addressId, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE customerId = @customerId";
                    //        using (var cmd = new MySqlCommand(updateCustomerQuery, conn, transaction))
                    //        {
                    //            cmd.Parameters.AddWithValue("@customerName", customerName);
                    //            cmd.Parameters.AddWithValue("@addressId", addressId);
                    //            //cmd.Parameters.AddWithValue("@active", "yes");
                    //            //cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                    //            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                    //            cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                    //            cmd.ExecuteNonQuery();
                    //        }
                    //        //}
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK);
                }
                }
            }
        public void UpdateCustomerDetails(string updatedCustomerName, string updatedAddress, string updatedPhone, string updatedCity, string updatedCountry)
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (var conn = new MySqlConnection(loginstr))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    
                    string getCountryIdQuery = "SELECT countryId FROM country WHERE country = @country";
                    MySqlCommand getCountryCmd = new MySqlCommand(getCountryIdQuery, conn, transaction);
                    getCountryCmd.Parameters.AddWithValue("@country", updatedCountry);
                    object countryResult = getCountryCmd.ExecuteScalar();
                    int countryId;
                    if (countryResult == null)
                    {
                        string insertCountryQuery = "INSERT INTO country (country) VALUES (@country)";
                        MySqlCommand insertCountryCmd = new MySqlCommand(insertCountryQuery, conn, transaction);
                        insertCountryCmd.Parameters.AddWithValue("@country", updatedCountry);
                        insertCountryCmd.ExecuteNonQuery();
                        string lastInsertQuery = "SELECT LAST_INSERT_ID();";
                        using (MySqlCommand cmdLastInsert = new MySqlCommand(lastInsertQuery, conn, transaction))
                        {
                            countryId = (int)insertCountryCmd.LastInsertedId;
                        }

                        //countryId = (int)insertCountryCmd.LastInsertedId;
                    }
                    else
                    {
                        countryId = Convert.ToInt32(countryResult);
                    }
                    string getCityIdQuery = "SELECT cityId FROM city WHERE city = @city AND countryId = @countryId";
                    MySqlCommand getCityCmd = new MySqlCommand(getCityIdQuery, conn, transaction);
                    getCityCmd.Parameters.AddWithValue("@city", updatedCity);
                    getCityCmd.Parameters.AddWithValue("@countryId", countryId);
                    object cityResult = getCityCmd.ExecuteScalar();
                    int cityId;
                    if (cityResult == null)
                    {
                        string insertCityQuery = "INSERT INTO city (city, countryId) VALUES (@city, @countryId)";
                        MySqlCommand insertCityCmd = new MySqlCommand(insertCityQuery, conn, transaction);
                        insertCityCmd.Parameters.AddWithValue("@city", updatedCity);
                        insertCityCmd.Parameters.AddWithValue("@countryId", countryId);
                        insertCityCmd.ExecuteNonQuery();

                        string lastInsertQuery = "SELECT LAST_INSERT_ID();";
                        using (MySqlCommand cmdLastInsert = new MySqlCommand(lastInsertQuery, conn, transaction))
                        {
                            cityId = (int)insertCityCmd.LastInsertedId;
                        }

                        //cityId = (int)insertCityCmd.LastInsertedId;
                    }
                    else
                    {
                        cityId = Convert.ToInt32(cityResult);
                        string updateCityQuery = "UPDATE city SET city = @city, countryId = @countryId WHERE cityId = @cityId";
                        MySqlCommand updateCityCmd = new MySqlCommand(updateCityQuery, conn, transaction);
                        updateCityCmd.Parameters.AddWithValue("@city", updatedCity);
                        updateCityCmd.Parameters.AddWithValue("@countryId", countryId);
                        updateCityCmd.Parameters.AddWithValue("@cityId", cityId);
                        updateCityCmd.ExecuteNonQuery();
                    }
                    string updateAddressQuery = @"UPDATE address SET address = @address, phone = @phone, cityId = @cityId WHERE address = (SELECT address FROM customer WHERE customerName = @oldcustomerName)";
                    MySqlCommand updateAddressCmd = new MySqlCommand(updateAddressQuery, conn, transaction);
                    updateAddressCmd.Parameters.AddWithValue("@address", updatedAddress);
                    updateAddressCmd.Parameters.AddWithValue("@phone", updatedPhone);
                    updateAddressCmd.Parameters.AddWithValue("@cityId", cityId);
                    //updateAddressCmd.Parameters.AddWithValue("@customerId", customerId);
                    updateAddressCmd.ExecuteNonQuery();

                    string updateCustomerQuery = @"UPDATE customer SET customerName = @updatedCustomerName, addressId = (SELECT addressId FROM address WHERE address = @address) WHERE customerName = @oldCustomerName";
                    MySqlCommand updateCustomerCmd = new MySqlCommand(updateCustomerQuery, conn, transaction);
                    updateCustomerCmd.Parameters.AddWithValue("@updatedCustomerName", updatedCustomerName);
                    //updateCustomerCmd.Parameters.AddWithValue("@address", updatedAddress);
                    //updateCustomerCmd.Parameters.AddWithValue("@addressId", addressId);
                    //updateCustomerCmd.Parameters.AddWithValue("@customerId", customerId);
                    updateCustomerCmd.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine($"Customer '{updatedCustomerName}' updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    transaction.Rollback();
                }
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void phoneTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }
    }
}
