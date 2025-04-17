using Ketkeorasmy_C969Project.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ketkeorasmy_C969Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DatabaseConnection.startConnection();
            Application.Run(new Form1());
            DatabaseConnection.closeConnection();
        }
    }
}
