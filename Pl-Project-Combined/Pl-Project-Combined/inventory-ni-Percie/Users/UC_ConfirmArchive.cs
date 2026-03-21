using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace inventory_ni_Percie
{
    /// <summary>
    /// Borderless modal dialog matching the screenshot:
    /// amber archive icon → "Archive User?" header → sub-text → Cancel / Confirm Archive.
    /// 
    /// Show it like this:
    ///   var modal = new UC_ConfirmArchive("admin");
    ///   modal.ShowDialog(ownerForm);
    ///   if (modal.IsConfirmed) { ... }
    /// </summary>
    public partial class UC_ConfirmArchive : Form
    {
        /// <summary>True when the user clicked "Confirm Archive".</summary>
        public bool IsConfirmed { get; private set; }

        private readonly string _username;

        public UC_ConfirmArchive(string username)
        {
            _username = username;
            InitializeComponent();

            // Inject the username into the sub-label at runtime
            lblSubText.Text = $"Archive <b>{username}</b>? This user will be moved to the\n" +
                              "Archive List and lose system access.";
        }

        // ── Button handlers ───────────────────────────────────────────────────
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IsConfirmed = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}