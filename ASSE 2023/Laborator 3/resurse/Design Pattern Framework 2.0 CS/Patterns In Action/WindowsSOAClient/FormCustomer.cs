using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsSOAClient
{
    /// <summary>
    /// This form is used to add new customer data or edit 
    /// existing customer data. 
    /// </summary>
    public partial class FormCustomer : Form
    {
        /// <summary>
        /// Default constructor of FormCustomer.
        /// </summary>
        public FormCustomer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets customer (company) name.
        /// </summary>
        public string Company
        {
            get { return textBoxCompany.Text.Trim(); }
            set { textBoxCompany.Text = value; }
        }

        /// <summary>
        /// Gets or sets customer city.
        /// </summary>
        public string City
        {
            get { return textBoxCity.Text.Trim(); }
            set { textBoxCity.Text = value; }
        }

        /// <summary>
        /// Gets or set customer country.
        /// </summary>
        public string Country
        {
            get { return textBoxCountry.Text.Trim(); }
            set { textBoxCountry.Text = value; }
        }

        /// <summary>
        /// Validates user input and, if valid, closes window. 
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CompanyName) ||
                String.IsNullOrEmpty(City) ||
                String.IsNullOrEmpty(Country))
            {
                // Do not close the dialog 
                MessageBox.Show("All fields are required");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormCustomer_Shown(object sender, EventArgs e)
        {
            // Check if this a new customer or existing one
            if ((Company + City + Country).Length == 0)
                this.Text = "New Customer";
            else
                this.Text = "Edit Customer";
        }
    }
}