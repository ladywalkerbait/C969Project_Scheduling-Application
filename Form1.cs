using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Data.SqlClient;
using System.IO;

namespace Ketkeorasmy_C969Project
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Languages();

        }
        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    Languages();
        //}

        private void Languages()
        {
            //switch (RegionInfo.CurrentRegion.TwoLetterISORegionName)
            //{
            //    case "FR":
            //        //label1.Text = "Username";
            //        //label2.Text = "Password";
            //        //LoginButton.Text = "Login";
            //        label1.Text = "Nom d'utilisateur";
            //        label2.Text = "Mot de passe";
            //        LoginButton.Text = "Se connecter";
            //        break;
            //    default:
            //        label1.Text = "Username";
            //        label2.Text = "Password";
            //        LoginButton.Text = "Login";
            //        //label1.Text = "Nom d'utilisateur";
            //        //label2.Text = "Mot de passe";
            //        //LoginButton.Text = "Se connecter";
            //        break;
            //}

            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            if (currentCulture.Name.StartsWith("fr"))
            {
                SetLanguage("fr-FR");
            }
            else
            {
                SetLanguage("en-EN");
            }
        }
        private void SetLanguage(string cultureCode)
        {
            CultureInfo cultureInfo = new CultureInfo(cultureCode);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            label1.Text = LanguageResources.Language.UsernameLabel;
            label2.Text = LanguageResources.Language.PasswordLabel;
            LoginButton.Text = LanguageResources.Language.LogButton;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            //get connection string
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

            //Make connection
            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(loginstr);

                //open connection
                conn.Open();


                //MessageBox.Show("Connection is open");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                //close connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            string userName = userNameTextBox.Text;
            string password = passwordTextBox.Text;
            verifyUser(userName, password);
           
            //LoginHistory(userName);

            //CheckAppointmentsForUser(userName);
        
        }
        private void verifyUser(string _userName, string _password)
        {
            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(loginstr))
            {
                try
                {
                    conn.Open();

                    string query = $"SELECT * FROM user WHERE userName = @userName AND password = @password";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@userName", _userName);
                        command.Parameters.AddWithValue("@password", _password);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count > 0)
                        {
                            Helpers.CurrentUser = _userName;
                            LoginHistory(_userName);
                            CheckAppointmentsForUser(_userName);
                            // MessageBox.Show("Login was successful");
                            MessageBox.Show(LanguageResources.Language.SuccessfulLogin);

                            Form2 form2 = new Form2();
                            form2.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                           // MessageBox.Show("Login was not successful");
                           MessageBox.Show(LanguageResources.Language.LoginFailure);
                            refreshForm1();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occured: {ex.Message}");
                }
            }
        }
        private void CheckAppointmentsForUser(string userName)
        {
            DateTime loginTime = DateTime.UtcNow;
            DateTime alertTimeLimit = loginTime.AddMinutes(15);

            string loginstr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(loginstr))
            {
                conn.Open();
                string checkQuery = @"SELECT a.start, c.customerName FROM appointment a
                                    JOIN customer c ON a.customerId = c.customerId
                                    WHERE a.userId = (SELECT userId FROM user WHERE userName = @userName)
                                    AND start BETWEEN @LoginTime AND @AlertTimeLimit";
                using (MySqlCommand command = new MySqlCommand(checkQuery, conn))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    //command.Parameters.AddWithValue("@customerName", customerName);
                    command.Parameters.AddWithValue("@LoginTime", loginTime);
                    command.Parameters.AddWithValue("@AlertTimeLimit", alertTimeLimit);
                    try
                    {
                        MySqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            string customerName = reader.GetString(reader.GetOrdinal("customerName"));
                            if (reader.HasRows)
                            {
                                MessageBox.Show($"{userName} has an appointment with {customerName} within 15 minutes!", "Appointment Alert", MessageBoxButtons.OK);
                            }
                            else
                            {
                                //MessageBox.Show($"No upcoming appointments for {userName}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error checking appointments: " + ex.Message, "Error", MessageBoxButtons.OK);
                    }
                }
            }
        }
        private void refreshForm1()
        {
            userNameTextBox.Text = string.Empty;
            passwordTextBox.Text = string.Empty;
            userNameTextBox.Focus();
        }

        private void userNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        public void LoginHistory(string userName) //need to log if the user was unsuccessful
        {
            string filePath = @"Login_History.txt"; //path to my log file
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {userName} logged in.";
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    sw.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing to log file: " + ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
