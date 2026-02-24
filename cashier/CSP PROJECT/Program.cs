using System;
using System.Windows.Forms;
using POSCashierSystem;

namespace CSP_PROJECT
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new POSCashierSystem.POSCashier());
        }
    }
}
