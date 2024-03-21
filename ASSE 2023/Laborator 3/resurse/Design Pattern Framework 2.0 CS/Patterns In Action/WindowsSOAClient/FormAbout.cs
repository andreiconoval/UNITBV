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
    /// Displays information about the SOA windows client application.
    /// </summary>
    public partial class FormAbout : Form
    {
        /// <summary>
        /// Default contructor for FormAbout.
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
        }

        // Close dialog.
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}