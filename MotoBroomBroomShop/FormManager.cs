using System;
using System.Windows.Forms;

namespace MotoDealerShop
{
    /// <summary>
    /// Manages the creation and display of all forms to prevent duplicate instances
    /// </summary>
    public static class FormManager
    {
        // Static instances - created once, reused forever
        private static UC_Overview overviewForm;
        private static UC_Candidates candidatesForm;
        private static UC_RecoveryOps recoveryOpsForm;

        // Track the currently visible form
        private static Form currentVisibleForm;

        /// <summary>
        /// Gets or creates the Overview form
        /// </summary>
        public static UC_Overview GetOverviewForm()
        {
            if (overviewForm == null || overviewForm.IsDisposed)
            {
                overviewForm = new UC_Overview();
            }
            return overviewForm;
        }

        /// <summary>
        /// Gets or creates the Candidates form
        /// </summary>
        public static UC_Candidates GetCandidatesForm()
        {
            if (candidatesForm == null || candidatesForm.IsDisposed)
            {
                candidatesForm = new UC_Candidates();
            }
            return candidatesForm;
        }

        /// <summary>
        /// Gets or creates the RecoveryOps form
        /// </summary>
        public static UC_RecoveryOps GetRecoveryOpsForm()
        {
            if (recoveryOpsForm == null || recoveryOpsForm.IsDisposed)
            {
                recoveryOpsForm = new UC_RecoveryOps();
            }
            return recoveryOpsForm;
        }

        /// <summary>
        /// Show the Overview form and hide others
        /// </summary>
        public static void ShowOverview()
        {
            var form = GetOverviewForm();
            ShowForm(form);
        }

        /// <summary>
        /// Show the Candidates form and hide others
        /// </summary>
        public static void ShowCandidates()
        {
            var form = GetCandidatesForm();
            ShowForm(form);
        }

        /// <summary>
        /// Show the RecoveryOps form and hide others
        /// </summary>
        public static void ShowRecoveryOps()
        {
            var form = GetRecoveryOpsForm();
            ShowForm(form);
        }

        /// <summary>
        /// Core method to show a form and hide all others
        /// </summary>
        private static void ShowForm(Form formToShow)
        {
            try
            {
                // Hide the previously visible form
                if (currentVisibleForm != null && !currentVisibleForm.IsDisposed)
                {
                    currentVisibleForm.Hide();
                }

                // Show the new form
                if (formToShow != null && !formToShow.IsDisposed)
                {
                    formToShow.Show();
                    formToShow.BringToFront();
                    currentVisibleForm = formToShow;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Close all managed forms
        /// </summary>
        public static void CloseAllForms()
        {
            if (overviewForm != null && !overviewForm.IsDisposed)
            {
                overviewForm.Close();
            }

            if (candidatesForm != null && !candidatesForm.IsDisposed)
            {
                candidatesForm.Close();
            }

            if (recoveryOpsForm != null && !recoveryOpsForm.IsDisposed)
            {
                recoveryOpsForm.Close();
            }

            currentVisibleForm = null;
        }
    }
}
