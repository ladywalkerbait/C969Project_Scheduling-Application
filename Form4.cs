using Ketkeorasmy_C969Project.Database;
using MySql.Data.MySqlClient;
using System;
using System.CodeDom;
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
    public partial class Form4 : Form
    {
        private Form3 _form3;

        public Form4(Form3 form3)
        {
            InitializeComponent();

            //idBox1.Text = Form3.GetCustomerID().ToString();
            //idBox1.ReadOnly = true;
            _form3 = form3;
        }

        private void nameBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameBox1.Text))
            {
                addCustomerButton.Enabled = false;
            }
        }

        private void addressBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cityBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void countryBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void phoneBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //Ignore cityLabel
        private void cityLabel_Click(object sender, EventArgs e)
        {

        }

        private void addCustomerButton_Click(object sender, EventArgs e)
        {  
            //fields are trimmed
            string customerName = nameBox1.Text.Trim();
            string address = addressBox1.Text.Trim();
            string city = cityBox1.Text.Trim();
            string country = countryBox1.Text.Trim();
            string phone = phoneBox1.Text.Trim();

            //prevents empty fields
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Customer name is required.");
                nameBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Address is required.");
                addressBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("City is required.");
                cityBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty (country))
            {
                MessageBox.Show("Country is required.");
                countryBox1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Phone is required.");
                phoneBox1.Focus();
                return;
            }

            _form3.addCustomerToTheDatabase(customerName, address, city, country, phone);
           // _form3.addCustomerToTheDGV(customerName, address, city, country, phone);

            this.Close();
            Form3 form3 = new Form3();
            form3.Refresh();
            form3.Show();
        }

        private void cancelAddCustomerButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nameBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                return;
            }
        }

        private void addressBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                return;
            }
        }

        private void cityBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void countryBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void phoneBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            { 
                e.Handled = true;
            }
        }

        private void idBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
