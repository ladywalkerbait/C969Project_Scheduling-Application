using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Ketkeorasmy_C969Project
{
    public static class Helpers
    {
        public static string CurrentUser { get; set; }
        // public static int GetCustomerId {  get; set; }

        public static int GetCountryId(MySqlConnection conn, MySqlTransaction transaction, string country)
        {
            //int countryId = -1;

            string checkCountryQuery = "SELECT countryId FROM country WHERE country = @country LIMIT 1";
            //try
            //{
            using (MySqlCommand cmd = new MySqlCommand(checkCountryQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@country", country);
                var result = cmd.ExecuteScalar();
                //object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    //countryId = Convert.ToInt32(result);
                    return Convert.ToInt32(result);
                }
                //return countryId;
                return -1;
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error fetching or inserting countryId: " + ex.Message);
            //}
            // return countryId;
        }
        public static int InsertCountry(MySqlConnection conn, MySqlTransaction transaction, string country)
        {
            string insertCountryQuery = "INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy)" + "VALUES (@country, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";
            using (MySqlCommand cmd = new MySqlCommand(insertCountryQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                //return Convert.ToInt32(cmd.ExecuteScalar());
                cmd.ExecuteNonQuery();
                string lastInsertQuery = "SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmdLastInsert = new MySqlCommand(lastInsertQuery, conn, transaction))
                {
                    return Convert.ToInt32(cmdLastInsert.ExecuteScalar());
                }
            }
        }
        public static int GetCityId(MySqlConnection conn, MySqlTransaction transaction, string city, int countryId)
        {
            int cityId = -1;

            string checkCityQuery = "SELECT city.cityId FROM city WHERE city.city = @city AND countryId = @countryId LIMIT 1";
            using (MySqlCommand cmd = new MySqlCommand(checkCityQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                //var result = cmd.ExecuteScalar();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    cityId = Convert.ToInt32(result);
                }
                return cityId;
            }
        }
        public static int InsertCity(MySqlConnection conn, MySqlTransaction transaction, string city, int countryId)
        {
            //string getCityQuery = "SELECT cityId FROM city WHERE city = @city AND countryId = @countryId";
            //using (MySqlCommand getcmd = new MySqlCommand(getCityQuery, conn, transaction))
            //{
            //getcmd.Parameters.AddWithValue("@city", city);
            //getcmd.Parameters.AddWithValue("@countryId", countryId);
            //var result = getcmd.ExecuteScalar();
            //if (result != null)
            //{
            //    return Convert.ToInt32(result);
            //}
            //else
            //{

            //insert city
            string insertCityQuery = "INSERT INTO city (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy)" + "VALUES (@city, @countryId, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";
            using (MySqlCommand cmd = new MySqlCommand(insertCityQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@countryId", countryId);
                cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                //return Convert.ToInt32(cmd.ExecuteScalar());
                cmd.ExecuteNonQuery();
                string lastInsertQuery = "SELECT LAST_INSERT_ID();";
                using (MySqlCommand cmdLastInsert = new MySqlCommand(lastInsertQuery, conn, transaction))
                {
                    return Convert.ToInt32(cmdLastInsert.ExecuteScalar());
                }
            }
            // }
            //}
        }
        public static int GetAddressId(MySqlConnection conn, MySqlTransaction transaction, string address)
        {
            int addressId = -1;

            string checkAddressQuery = "SELECT address.addressId FROM address WHERE address.address = @address LIMIT 1";
            using (MySqlCommand cmd = new MySqlCommand(checkAddressQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@address", address);
                //cmd.Parameters.AddWithValue("@customerId", cityId);
                //var result = cmd.ExecuteScalar();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    addressId = Convert.ToInt32(result);
                }
                return addressId;
            }
        }
        public static int InsertAddress(MySqlConnection conn, MySqlTransaction transaction, string address, int cityId, string phone)
        {
            string getAddressQuery = "SELECT addressId FROM address WHERE address = @address AND cityId = @cityId";
            using (MySqlCommand getcmd = new MySqlCommand(getAddressQuery, conn, transaction))
            {
                getcmd.Parameters.AddWithValue("@address", address);
                getcmd.Parameters.AddWithValue("@cityId", cityId);
                var result = getcmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {

                    //insert address
                    string insertAddressQuery = "INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)" + "VALUES (@address, @address2, @cityId, @postalCode, @phone, @createDate, @createdBy, @lastUpdate, @lastUpdateBy)";
                    using (MySqlCommand cmd = new MySqlCommand(insertAddressQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@address2", "Not important"); //check this
                        cmd.Parameters.AddWithValue("@cityId", cityId);
                        cmd.Parameters.AddWithValue("@postalCode", "postalCode"); //check this
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                        cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                        //return Convert.ToInt32(cmd.ExecuteScalar());
                        cmd.ExecuteNonQuery();
                        string lastInsertQuery = "SELECT LAST_INSERT_ID();";
                        using (MySqlCommand cmdLastInsert = new MySqlCommand(lastInsertQuery, conn, transaction))
                        {
                            return Convert.ToInt32(cmdLastInsert.ExecuteScalar());
                        }
                    }
                }
            }
        }
        //testing the below method
        public static void UpdateAddress(MySqlConnection conn, MySqlTransaction transaction,int addressId, string address, int cityId, string phone)
        {
            string updateQuery = "UPDATE address SET address = @address, cityId = @cityId, phone = @phone WHERE addressId = @addressId";
            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@addressId", addressId);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@cityId", cityId);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();
            }
        }
        public static void InsertCustomer(MySqlConnection conn, MySqlTransaction transaction, string customerName, int addressId)
        {
            string insertCustomerQuery = "INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)" + "VALUES (@customerName, @addressId, @active,@createDate, @createdBy, @lastUpdate, @lastUpdateBy)";
            using (MySqlCommand cmd = new MySqlCommand(insertCustomerQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@customerName", customerName);
                cmd.Parameters.AddWithValue("@addressId", addressId);
                cmd.Parameters.AddWithValue("@active", "1"); //check this
                cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                cmd.ExecuteNonQuery();
            }
        }
        //Testing below method
        public static void UpdateCustomerInfo(MySqlConnection conn, MySqlTransaction transaction, string customerName, int addressId)
        {
            string updateQuery = "UPDATE customer SET customerName = @customerName, addressId = @addressId WHERE customerId = @customerId";
            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@customerName", customerName);
                cmd.Parameters.AddWithValue("@addressId", addressId);
                //cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateCustomer(string customerName, string address, string city, string country, string phone)
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
                    int countryId = GetCountryId(conn, transaction, country);
                    if (countryId == -1)
                    {
                        countryId = Helpers.InsertCountry(conn, transaction, country);
                        countryId = GetCountryId(conn, transaction, country); //testing
                    }
                    else
                    {
                        string updateCountryQuery = @"UPDATE country SET country = @country, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE countryId = @countryId";
                        using (var cmd = new MySqlCommand(updateCountryQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@country", country);
                            //cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                            //cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@lasUpdateBy", Helpers.CurrentUser);
                            cmd.Parameters.AddWithValue("@countryId", countryId); //added this in to check
                            cmd.ExecuteNonQuery();
                        }
                    }
                    int cityId = GetCityId(conn, transaction, city, countryId);
                    if (cityId == -1)
                    {
                        cityId = Helpers.InsertCity(conn, transaction, city, countryId);
                        cityId = GetCityId(conn, transaction, city, countryId);
                    }
                    else
                    {
                        string updateCityQuery = @"UPDATE city SET city = @city, countryId = @countryId, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE cityId = @cityId";
                        using (var cmd = new MySqlCommand(updateCityQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@city", city);
                            cmd.Parameters.AddWithValue("@countryId", countryId);
                            //cmd.Parameters.AddWithValue("@createDate", DateTime.Now);
                            //cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                            cmd.ExecuteNonQuery();
                        }
                    }
                        int addressId = GetAddressId(conn, transaction, address);
                        if (addressId == -1)
                        {
                            addressId = Helpers.InsertAddress(conn, transaction, address, cityId, phone);
                            addressId = GetAddressId(conn, transaction, address);
                        }
                        else
                        {
                            string updateAddressQuery = @"UPDATE address SET address = @address, cityId = @cityId, phone = @phone, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE addressId = @addressId";
                            using (var cmd = new MySqlCommand(updateAddressQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@address", address);
                                cmd.Parameters.AddWithValue("@cityId", cityId);
                                cmd.Parameters.AddWithValue("@phone", phone);
                                cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                                cmd.Parameters.AddWithValue("@addressId", addressId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    int customerId = GetCustomerId(conn, transaction, address);
                    //if (customerId == -1)
                    //{
                    //    //customerId = Helpers.GetCustomerId(conn, transaction, address);
                    //    //customerId = Helpers.InsertCustomer(conn, transaction, customerName, addressId);
                    //}
                    //else
                    //{
                        string updateCustomerQuery = @"UPDATE customer SET customerName = @customerName, addressId = @addressId, lastUpdate = @lastUpdate, lastUpdateBy = @lastUpdateBy WHERE customerId = @customerId";
                        using (var cmd = new MySqlCommand(updateCustomerQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@customerName", customerName);
                            cmd.Parameters.AddWithValue("@addressId", addressId);
                            //cmd.Parameters.AddWithValue("@active", "yes");
                            //cmd.Parameters.AddWithValue("@createdBy", Helpers.CurrentUser);
                            cmd.Parameters.AddWithValue("@lastUpdate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@lastUpdateBy", Helpers.CurrentUser);
                            cmd.ExecuteNonQuery();
                        }
                        //}
                        transaction.Commit();
                        MessageBox.Show("Customer information updated successfully", "Success", MessageBoxButtons.OK);
                    //}
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error updating customer: " + ex.Message);
                }
            }
        }
        public static int GetCustomerId(MySqlConnection conn, MySqlTransaction transaction, string customerName)
        {
            //int customerId = -1;
            //try
            //{
                string checkCustomerQuery = "SELECT customerId FROM customer WHERE customerName = @customerName LIMIT 1";
                //try
                //{
                using (MySqlCommand cmd = new MySqlCommand(checkCustomerQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@customerName", customerName);
                    //var result = cmd.ExecuteScalar();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        //customerId = Convert.ToInt32(result);
                        return Convert.ToInt32(result);
                    }
                    //return customerId;
                    else
                    {
                        return -1;
                    }
                }
            //}
            //catch (Exception ex) { Console.WriteLine("Error getting customerId: " + ex.Message); return -1; }
        }

        public static void deleteCustomerFromDatabase()
        {

        }
    }
}
    
