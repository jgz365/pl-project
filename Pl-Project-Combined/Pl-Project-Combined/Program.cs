// ═══════════════════════════════════════
// FILE: Program.cs
// PURPOSE: App entry point — init DB first, then open LoginUiForm
// ═══════════════════════════════════════
using inventory_ni_Percie;
using System;
using System.Windows.Forms;

namespace Pl_Project_Combined
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            ApplicationConfiguration.Initialize();

            // ── STEP 1: Initialize the database BEFORE showing any form ──
            // This creates the DB, tables, and seeds superadmin + admin
            // if they don't exist yet. Shows a user-friendly error if
            // XAMPP/MySQL is not running.
            try
            {
                DatabaseManager.Initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Could not connect to the database.\n\n" +
                    $"Make sure XAMPP is running and MySQL is started.\n\n" +
                    $"Error: {ex.Message}",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return; // Exit — no point opening the app without a DB
            }

            // ── STEP 2: Open the login form ──
            //Application.Run(new Pl_Project_Combined.Assessor_Eddion.LoginUiForm());

            // Temporarily launch the customer kiosk start screen (hosted in main_menu)
            // Comment out the line above to revert back to the original LoginUiForm.
            Application.Run(new customer_kiosk.main_menu());
        }
    }
}