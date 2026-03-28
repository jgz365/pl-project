using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    /// <summary>
    /// Red-themed permanent delete confirmation modal.
    /// Add as a Windows Form (not UserControl) in VS.
    ///
    /// Usage:
    ///   using (var overlay = new FormBlurOverlay(mainForm))
    ///   {
    ///       overlay.Show(mainForm);
    ///       using var modal = new UC_ConfirmDelete("admin");
    ///       modal.ShowDialog(mainForm);
    ///       if (modal.IsConfirmed) { /* permanently delete */ }
    ///   }
    /// </summary>
    public partial class UC_ConfirmDelete : Form
    {
        /// <summary>True when the user clicked "Delete".</summary>
        public bool IsConfirmed { get; private set; }

        public UC_ConfirmDelete(string username)
        {
            InitializeComponent();

            // Inject bold username into sub-label at runtime
            lblSubText.Text = $"Are you sure you want to permanently delete <b>{username}</b>?\nThis action cannot be undone.";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}