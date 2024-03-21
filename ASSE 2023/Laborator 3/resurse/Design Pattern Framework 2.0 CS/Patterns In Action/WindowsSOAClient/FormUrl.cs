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
    /// This is where users can get and set the web service Url. This may be 
    /// necessary if the web service url is not at the standard location 
    /// (as specified in app.config).
    /// </summary>
    public partial class FormUrl : Form
    {
        /// <summary>
        /// Default constructor of FormUrl.
        /// </summary>
        public FormUrl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Displays current web service URL in dialog.
        /// </summary>
        private void FormUrl_Load(object sender, EventArgs e)
        {
            this.textBoxServiceUrl.Text = ServiceUrl.Url;
        }

        /// <summary>
        /// Stores Url setting and closes dialog.
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            ServiceUrl.Url = this.textBoxServiceUrl.Text.Trim();
            this.Close();
        }

        /// <summary>
        /// Cancels operation and closes dialog. 
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}