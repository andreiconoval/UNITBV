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
    /// This is where users enter their credentials and security token to be authenticated
    /// on the web service. For convenience credentials have been pre-populated. 
    /// They are: 
    /// userName: Jill
    /// password: LochNess24
    /// security token: ABC123
    /// </summary>
    public partial class FormLogin : Form
    {
        /// <summary>
        /// Default contructor of FormLogin.
        /// </summary>
        public FormLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets username.
        /// </summary>
        public string UserName
        {
            get { return textBoxUserName.Text.Trim(); }
        }

        /// <summary>
        /// Gets password.
        /// </summary>
        public string Password
        {
            get { return textBoxPassword.Text.Trim(); }
        }

        /// <summary>
        /// Gets security token.
        /// </summary>
        public string SecurityToken
        {
            get { return textBoxSecurityToken.Text.Trim(); }
        }

        /// <summary>
        /// Closes dialog with OK return value.
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Closes dialog with Cancel return value.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Opens web service Url dialog.
        /// </summary>
        private void linkLabelWebService_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FormUrl form = new FormUrl())
            {
                form.ShowDialog();
            }
        }

        private void linkLabelValid_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("For this reference application there is just one set \r\nof credentials. The fields in this dialog are \r\nprepopulated with these values. They are: \r\n\r\n" +
                "    UserName:    Jill \r\n" +
                "    PassWord:    LochNess24 \r\n" +
                "    Security Token: ABC123\r\n\r\n" +
                "You could also debug the Web Service and see what \r\ncredentials are accepted", "Login Credentials");
        }
    }
}