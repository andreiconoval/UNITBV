using System;
using System.Windows.Forms;

namespace DemoDataAccessApplicationBlock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
