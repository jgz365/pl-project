using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MotoDealerShop
{
    // ── IMPORTANT: Must be UserControl, NOT Form ─────────────────────────────
    public partial class UC_RecoveryOps : UserControl
    {
        public UC_RecoveryOps()
        {
            InitializeComponent();
        }

        private void LoadRecoveryOperations()
        {
            try
            {
                // TODO: Load data from database or API
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recovery operations: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshOperations()
        {
            LoadRecoveryOperations();
        }

        public void UpdateOperationStatus(int operationId, string newStatus)
        {
            try
            {
                // TODO: Update operation status in database
                MessageBox.Show($"Operation {operationId} status updated to: {newStatus}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshOperations();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating operation status: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerateOperationsReport()
        {
            try
            {
                MessageBox.Show("Generating recovery operations report...", "Report",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SearchOperations(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    RefreshOperations();
                    return;
                }
                // Perform search on current data
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching operations: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── Shared helper: routes through whichever host Form is active ───────
        private void NavigateTo(UserControl uc)
        {
            var form = this.FindForm();
            if (form is inventory_ni_Percie.Form1 f1)
                f1.DisplayPage(uc);
            else if (form is inventory_ni_Percie.Form2 f2)
                f2.DisplayPage(uc);
        }

        // ── Tab button navigation ─────────────────────────────────────────────
        private void OBT_Click(object sender, EventArgs e)
        {
            NavigateTo(new UC_Overview());
        }

        private void CBT_Click(object sender, EventArgs e)
        {
            NavigateTo(new UC_Candidates());
        }

        private void ROBT_Click(object sender, EventArgs e)
        {
            NavigateTo(new UC_RecoveryOps());
        }

        private void ActiveRecoveryOperations_Click(object sender, EventArgs e) { }

        private void tablelayoutpanel_Paint(object sender, PaintEventArgs e) { }
    }
}