using System;
using System.Windows.Forms;

namespace MotoDealerShop
{
    /// <summary>
    /// Manages the creation and display of all user controls to prevent duplicate instances
    /// </summary>
    public static class FormManager
    {
        // Static instances - created once, reused forever
        private static UC_Overview overviewForm;
        private static UC_Candidates candidatesForm;
        private static UC_RecoveryOps recoveryOpsForm;

        // Track the currently visible control
        private static UserControl currentVisibleControl;

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
        /// Core method to show a control and hide all others
        /// </summary>
        private static void ShowForm(UserControl controlToShow)
        {
            try
            {
                // Hide the previously visible control
                if (currentVisibleControl != null && !currentVisibleControl.IsDisposed)
                {
                    currentVisibleControl.Visible = false;
                }

                // Show the new control
                if (controlToShow != null && !controlToShow.IsDisposed)
                {
                    controlToShow.Visible = true;
                    controlToShow.BringToFront();
                    currentVisibleControl = controlToShow;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Close all managed controls
        /// </summary>
        public static void CloseAllForms()
        {
            if (overviewForm != null && !overviewForm.IsDisposed)
            {
                overviewForm.Dispose();
            }

            if (candidatesForm != null && !candidatesForm.IsDisposed)
            {
                candidatesForm.Dispose();
            }

            if (recoveryOpsForm != null && !recoveryOpsForm.IsDisposed)
            {
                recoveryOpsForm.Dispose();
            }

            currentVisibleControl = null;
        }
    }
}
